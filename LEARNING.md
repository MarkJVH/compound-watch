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
| C# async/await and `CancellationToken` | 0 | – | – |
| Dependency injection and options pattern | 0 | – | – |
| Minimal API design (routing, DTOs, validation, `ProblemDetails`) | 0 | – | – |
| EF Core mapping and constraints | 2 | Wrote `PriceQuoteConfiguration` (composite key, FK, `Restrict`, precision) from an `Instrument` example, with review. Quiz: `Restrict` direction right, missed that the database enforces it; `decimal` precision/scale right, missed why `decimal` beats `double` (session 2026-11-14) | 2026-11-14 |
| EF Core migrations | 0 | – | – |
| EF Core querying (tracking, projection, N+1) | 0 | – | – |
| Transactions and concurrency | 0 | – | – |
| PostgreSQL basics (indexes, query plans) | 1 | Explained B-tree, page, page split, cache miss, Guid v7 versus v4 (session 2026-11-14); own-words answers needed several corrections, so not higher | 2026-11-14 |
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
- [ ] Phase 2, step 3: write `WatchlistItemConfiguration` yourself, without an example, then explain in your own words why `decimal` and not `double` for prices.

## Session log

Newest first. Written by `cw-learn` after each session.

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
