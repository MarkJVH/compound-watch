# Learning progress

Goal: learn the core principles of this stack well enough to use and explain them without AI help. Progress is measured by **evidence**, not by what the AI built.

## Levels

| Level | Meaning | Evidence required |
|---|---|---|
| 0 | Unknown | – |
| 1 | Explained to me | Session log entry |
| 2 | Done with help | Commit or test where I wrote part of it |
| 3 | Done independently | Commit/test/PR I wrote myself; the AI only reviewed |
| 4 | Can explain and weigh trade-offs | Correct answers to `cw-learn` questions in my own words, or an ADR I wrote |

A level only goes up with evidence in the table. It may go down after a relapse (a concept I got wrong again).

## Competency matrix

| Concept | Level | Evidence | Last updated |
|---|---|---|---|
| C# language basics (value types, `default`, nullability, `nameof`, exceptions) | 2 | Wrote `Instrument`, `WatchlistItem`, `PriceQuote` myself with AI hints/review (commits `fba1968`, `fea7be3`) | 2026-11-13 |
| C# async/await and `CancellationToken` | 1 | Explained why `await` frees the thread during I/O and that `ct` cancels the query when the client aborts (session 2026-11-23); not yet in own words | 2026-11-23 |
| Dependency injection and options pattern | 2 | Wrote `AddInfrastructure` (`AddDbContext`, connection string fail-fast) and wired it in `Api` and `Worker` myself with hints (commit `03f2ab4`). Own words: one context for all environments through injected options. Mistake: thought a missing connection string fails at build time | 2026-11-18 |
| Minimal API design (routing, DTOs, validation, `ProblemDetails`) | 2 | Wrote `WatchlistEndpoints.cs` myself with hints: `MapGroup`, DI-bound handlers, request/response records, status codes 201/400/404/409 (commits `d5f3dc7`, `bb29599`). Mistake: thought my guard catches a missing header; model binding returns 400 before the handler runs. `ProblemDetails` still open | 2026-11-23 |
| EF Core mapping and constraints | 3 | Wrote `PriceQuoteConfiguration` from an `Instrument` example (2026-11-14), then `WatchlistItemConfiguration` (key, unique index, FK, `Cascade`) without an example; the AI only reviewed (first version had 3 compile errors and a wrong uniqueness argument, fixed after review). Own words: `decimal` is exact base-10, `double` is not | 2026-11-14 |
| EF Core migrations | 2 | Generated and applied `InitialCreate` with AI-given commands; read the C# migration and the real table in `psql`. Own words: `NOT NULL` source, `Restrict` versus `Cascade`, composite key order (needed a refinement) | 2026-11-18 |
| EF Core querying (tracking, projection, N+1) | 2 | Wrote the `AsNoTracking` + `Select` projection to a DTO and `AnyAsync` myself with hints (commit `bb29599`); deferred execution explained | 2026-11-23 |
| Transactions and concurrency | 1 | Own words, correct: check-then-act race on a duplicate POST, the unique index is the real guarantee, the catch maps the loser to 409 (session 2026-11-23) | 2026-11-23 |
| PostgreSQL basics (indexes, query plans) | 1 | Explained B-tree, page, page split, cache miss, Guid v7 versus v4 (session 2026-11-14); own-words answers needed several corrections, so not higher. Later same day, own words and correct: why a sequential scan beats an index on a tiny table; composite index order (leftmost prefix) needed one correction | 2026-11-14 |
| RabbitMQ (exchanges, queues, ack/nack) | 0 | – | – |
| Idempotency, retries and dead-lettering | 0 | – | – |
| Background workers and graceful shutdown | 0 | – | – |
| External HTTP clients and resilience | 0 | – | – |
| Testing (unit, integration, Testcontainers) | 2 | Wrote the `PriceQuote` test helper and failure-path tests myself (commit `888b9be`); AI added the simple variants | 2026-11-13 |
| Logging and observability | 0 | – | – |
| Docker and Compose | 1 | Phase 1 session | 2026-11-10 |
| Authentication and authorization | 0 | – | – |
| Next.js basics (later) | 0 | – | – |

Add rows when a new concept appears. Keep names specific.

## Exercises (done without AI)

- [x] Phase 2, step 2: write `IEntityTypeConfiguration<PriceQuote>` (done 2026-11-14 with an `Instrument` example and review) and explain why Guid v7 beats v4 for an index (done, after several corrections).
- [x] Phase 2, step 3: write `WatchlistItemConfiguration` yourself, without an example (done 2026-11-14, after review), then explain in your own words why `decimal` and not `double` for prices (done: exact versus not exact; missed binary versus base-10 as the cause).
- [x] Phase 2, step 4a: create `AppDbContext` and `InstrumentConfiguration` yourself (done 2026-11-15, after review: wrong `DbContextOptions<DbContext>`, a `HasKey` that overwrote the key, an index on `Id` instead of `Symbol`).
- [x] Phase 2, step 4b-1: write `AddInfrastructure` (`AddDbContext` + `UseNpgsql` + snake_case), call it from `Api` and `Worker`, set the connection string with user secrets (done 2026-11-16, after review: missing `this`, wrong auto-import).
- [x] Phase 2, step 4b-2: install `dotnet-ef`, generate the first migration (`InitialCreate`), apply it and explain the generated SQL (done 2026-11-18; `--startup-project` reason was explained by the AI, not yet answered by the owner).
- [x] Phase 2, step 5: `UserId` on `WatchlistItem`, unique `(UserId, InstrumentId)` mapping, second migration and the first Api endpoints (done 2026-11-23, with hints and review).
- [ ] Phase 2, step 6: write an integration test for `POST /watchlist` (201 and 409) with `WebApplicationFactory` and Testcontainers, without AI.

## Session log

Newest first. Written by `cw-learn` after each session.

### 2026-11-23 – Phase 2, step 5: watchlist endpoints
- Built: `UserId` on `WatchlistItem`, per-user unique index, `AddUserIdToWatchlistItem` migration, `POST` and `GET /watchlist` with an `X-User-Id` header, `catch` narrowed to a Postgres unique violation. Written by the owner; the AI reviewed.
- Concepts: static classes and extension methods, `MapGroup`, handler parameter binding, `record` versus class, `AsNoTracking`, projection and deferred execution, `async` I/O, 201/400/404/409 semantics, exception filters (`when`), user secrets location, `docker compose` and `psql`.
- Hard: `static`/`this`, `record` versus class, REST Client variable with a dot, missing header versus empty Guid.
- Quiz:
  - Why catch a unique violation despite a prior check: right, own words (race condition).
  - Missing `X-User-Id` header: answered incorrectly; model binding returns 400 before the handler runs, the guard only catches `Guid.Empty`.
- Next time I do myself: step 6 (see exercises).

### 2026-11-18 – Phase 2, step 4b-2: first migration
- Built: `dotnet-ef` installed, `Microsoft.EntityFrameworkCore.Design` in `Api`, `InitialCreate` generated and applied to the local Postgres.
- Concepts: design-time tooling and the startup project, `__EFMigrationsHistory`, `NOT NULL` origin, `Restrict` versus `Cascade` in SQL, composite primary key order, from mapping to migration to real table.
- Hard: zsh glob on `10.0.*`; tool version 11 versus packages 10.
- Quiz:
  - `nullable: false`, `onDelete: Restrict`, no `id` on `price_quotes`: mostly right; `Restrict` gives a foreign key error, not a silent no-op.
  - `Cascade` on `watchlist_items`: right.
  - Key order `(instrument_id, date)`: leftmost prefix right; the range-query argument was added by the AI.
  - Why `dotnet ef` needs the `Api`: not answered; explained by the AI.
- Next time I do myself: step 5 (see exercises).

### 2026-11-16 – Phase 2, step 4b-1: AddInfrastructure and user secrets
- Built: `AddInfrastructure` extension (connection string with a fail-fast check, `AddDbContext`), called from `Api` and `Worker`; connection string in user secrets. Written by the owner; the AI reviewed.
- Concepts: extension methods (`this`), compile-time versus runtime errors, dependency injection (register versus resolve), options reach the context through the constructor, one context for all environments via `IConfiguration`, library versus host (composition root), purpose of the Worker, user secrets (outside the repo, plain text), `dotnet run <file>.cs` versus `--project`.
- Hard: forgot `this`; wrong auto-import again; ran `dotnet run src/Api/Program.cs` (file-based app, no project references).
- Quiz:
  - Why a missing `this` shows up in `Program.cs`: right moment, reason refined (extension lookup at the call site).
  - Missing connection string: build or runtime: answered "build"; correct is runtime (config is not seen by the compiler).
  - Why one `DbContext` for all environments: right, own words.
  - Forgetting `AddInfrastructure` in the Worker: answered by the AI; not yet checked.
- Next time I do myself: step 4b-2 (see exercises). Small steps.

### 2026-11-15 – Phase 2, step 4a: AppDbContext and InstrumentConfiguration
- Built: `AppDbContext` (three `DbSet`s, `ApplyConfigurationsFromAssembly`) and `InstrumentConfiguration` (key, `ValueGeneratedNever`, unique index on `(Symbol, Exchange)`), written by the owner; the AI reviewed.
- Concepts: what belongs in a `DbContext`, `HasPrecision` versus `ConfigureConventions`, snake_case naming and PostgreSQL identifier quoting, `UseSnakeCaseNamingConvention` belongs in the options, composition root and an `AddInfrastructure` extension (DRY), surrogate versus natural key, DI resolves options by exact generic type at runtime.
- Hard: the second `HasKey` overwrites the first; the compiler does not see model mistakes; an index on `(Id, ...)` guards nothing. The owner reported information overload, so connection string/user secrets were deferred.
- Quiz:
  - Quoted `"WatchlistItems"` versus unquoted `watchlistitems`: right, own words.
  - Index order `(Symbol, Exchange)`: right, own argument (selectivity).
  - Why `DbContextOptions<DbContext>` fails only at runtime: not yet understood (answer given: DI container resolves by exact type).
  - Natural versus surrogate key, naming convention location, connection string key: answered by the AI after "don't know".
- Next time I do myself: step 4b (see exercises). Small steps, one topic at a time.

### 2026-11-14 – Phase 2, step 3: WatchlistItem EF Core mapping
- Built: `WatchlistItemConfiguration` (key, unique index on `InstrumentId`, FK to `Instrument` with `Cascade`), written by the owner without an example.
- Concepts: `:` as interface implementation, generic type parameter `TEntity`, assembly (compiled `.dll` with IL and metadata) and reflection in `ApplyConfigurationsFromAssembly`, `IsRequired()` versus nullable types, default `DeleteBehavior` (Cascade for a required FK, ClientSetNull for optional), one-to-many with a join table, composite index order (leftmost prefix), several indexes and the query planner, sequential scan versus index, Cascade does not notify the app (outbox pattern later).
- Hard: unique on `Id` does not stop the same instrument twice (each item gets its own `Id`); `HasIndex()`/`HasForeignKey()` need arguments; the FK direction (`User` 1→N `WatchlistItem` N←1 `Instrument`).
- Quiz:
  - `decimal` versus `double`: exact versus not exact (right). Missing cause: `double` is binary floating point, so 0.1 has no exact representation; `decimal` is base-10.
  - Composite index order: user first gives a contiguous range (right direction). Reversing it breaks the other query; use two indexes.
  - Sequential scan on a tiny table: right (one page, no index hop).
  - Outbox: event in the same transaction as the delete, so you never notify for a delete that did not complete, nor skip the notification for one that did (right, own words).
- Next time I do myself: `AppDbContext` + `InstrumentConfiguration` + first migration (see exercises). Design later: `UserId` on `WatchlistItem` (unique `(UserId, InstrumentId)` plus index on `InstrumentId`).

### 2026-11-14 – Phase 2, step 2: PriceQuote EF Core mapping
- Built: `PriceQuoteConfiguration` (composite key, FK to `Instrument` with `Restrict`, `HasPrecision(18, 6)`); English-jargon rule in `.github/context.md` and `cw-learn`.
- Concepts: `IEntityTypeConfiguration`, `HasOne().WithMany().HasForeignKey()` without navigation properties, `DeleteBehavior`, redundant `IsRequired()`/`ValueGeneratedNever()`, B-tree (not binary tree), page, leaf, split, cache miss, fragmentation, Guid v7 versus v4.
- Hard: B-tree versus binary tree; cache miss and page split are two separate causes; a leaf holds a key range but is full by entry count; why `decimal` beats `double`.
- Quiz:
  - `Remove(aapl)` with `Restrict`: right direction. Full answer: the migration contains `ON DELETE RESTRICT`, PostgreSQL rejects the `DELETE` with a foreign key violation, EF Core surfaces it as `DbUpdateException`, nothing is deleted (transaction). With `Cascade` PostgreSQL deletes the instrument and all its quotes silently.
  - `decimal` versus `double`: precision/scale right (18 total digits, 6 after the comma). Full answer: `double` is binary floating point and cannot represent values like 0.1 exactly (`0.1 + 0.2 = 0.30000000000000004`); `decimal` stores base-10 digits exactly, which is what you want for prices.
  - B-tree and Guid v7 versus v4: v7 inserts land in the rightmost leaf (hot in memory, leaves stay full); v4 inserts land randomly (cache miss, page split in the middle, half-empty leaves, bigger index). Lookups cost the same; v7 does not speed up queries by date.
- Next time I do myself: `WatchlistItemConfiguration` (see exercises).

### 2026-11-13 – Phase 2, step 1: PriceQuote and Domain tests
- Built: `PriceQuote` (OHLC + volume, factory with invariants) and its unit tests; ADRs 0001/0002.
- Concepts: composite keys, DB-enforced uniqueness vs domain validation, `null!`, value types and `default`, `nameof`/`ParamName`, `Assert.Throws` exact type vs `ThrowsAny`, test helper with optional parameters.
- Hard: `default` for a nullable parameter is `null`; a helper default of `new Guid()` made every throw-test pass for the wrong reason; `Assert.Throws` returns the exception, not the result.
- Quiz: exception types and `ParamName` (good); duplicate key handled in the DB, 1 call (good, race condition missed); Guid v7 vs v4 (named size cost, missed time-ordered index benefit).
- Next time I do myself: the EF Core mapping for `PriceQuote` (see exercises).

### 2026-11-10 – Phase 2, step 1: Instrument (in progress)
- Built: `Instrument` (rich domain model, factory method) and a first unit test.
- Concepts: C# classes and factory methods, invariants, `ArgumentException.ThrowIfNullOrWhiteSpace`, xUnit basics (Fact/Theory/Assert).
- Hard: constructor vs private constructor + static `Create`; `null!` and nullable warnings; normalising before validating.
- Next time I do myself: tests for the failure paths (empty values, invalid currency) and a test that proves `Name` is set.
- Matrix levels stay unchanged until the end of the phase (with evidence).

### 2026-11-10 – Phase 1: foundation
- Built: solution layout, Compose stack, `/health`.
- Concepts: Docker and Compose (level 1).
- Hard: –
- Next time I do myself: –
