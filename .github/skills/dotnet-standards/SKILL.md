---
name: dotnet-standards
description: 'Review and implementation checklist for this stack: ASP.NET Core, EF Core + PostgreSQL, RabbitMQ, background workers, tests. Used by cw-build and cw-review.'
---

# .NET standards

## General
- Nullable reference types on; no `!` to silence warnings without a reason.
- `async` all the way; no `.Result`/`.Wait()`. Pass `CancellationToken` through endpoints, handlers and EF calls.
- Inject `TimeProvider`, not `DateTime.UtcNow`. Store timestamps as UTC (`timestamptz`).
- Money and prices: `decimal` with explicit precision, never `double`. Store the currency alongside.
- Options pattern for config with validation on start; no magic strings for connection names.
- Structured logging (`logger.LogInformation("... {Symbol}", symbol)`), no string interpolation in log calls, no secrets or PII.

## API
- Validate input at the boundary once; return `ProblemDetails` for errors.
- DTOs for the contract; never expose EF entities.
- Correct status codes; idempotent `PUT`/`DELETE`; pagination on list endpoints.
- Minimal APIs grouped per feature (`MapGroup`), not one giant `Program.cs`.

## EF Core / PostgreSQL
- Domain entities are persistence-ignorant; mapping lives in `IEntityTypeConfiguration<T>` in Infrastructure.
- Constraints in the database too: keys, uniqueness, required, max lengths, foreign keys.
- `AsNoTracking()` on read-only queries; project to DTOs with `Select`; avoid N+1 (`Include`/projection).
- One migration per logical change, descriptive name, reviewed SQL; never edit an applied migration.
- Index what you filter and sort on.
- Tests against real Postgres (Testcontainers), not the in-memory provider.

## RabbitMQ / Worker
- Consumers must be idempotent (at-least-once delivery); use a dedupe key.
- Explicit ack/nack, dead-letter queue for poison messages, bounded retries with backoff.
- Durable queues, persistent messages for anything that must not be lost.
- Message contracts are versioned and live in Domain or a shared contracts project.
- Outbound HTTP via `IHttpClientFactory` with timeout and resilience (retry, circuit breaker); respect provider rate limits.
- Workers handle graceful shutdown via the stopping token.

## Tests
- Test behaviour, not implementation. Arrange/Act/Assert, one behaviour per test.
- Unit tests for domain logic; integration tests for endpoints (`WebApplicationFactory`) and data access.
- No sleeps; no shared mutable state between tests.

## Dependencies
- Prefer first-party and well-maintained packages. Run `dotnet list package --vulnerable --include-transitive` when adding or bumping.
