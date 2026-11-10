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
| C# async/await and `CancellationToken` | 0 | – | – |
| Dependency injection and options pattern | 0 | – | – |
| Minimal API design (routing, DTOs, validation, `ProblemDetails`) | 0 | – | – |
| EF Core mapping and constraints | 0 | – | – |
| EF Core migrations | 0 | – | – |
| EF Core querying (tracking, projection, N+1) | 0 | – | – |
| Transactions and concurrency | 0 | – | – |
| PostgreSQL basics (indexes, query plans) | 0 | – | – |
| RabbitMQ (exchanges, queues, ack/nack) | 0 | – | – |
| Idempotency, retries and dead-lettering | 0 | – | – |
| Background workers and graceful shutdown | 0 | – | – |
| External HTTP clients and resilience | 0 | – | – |
| Testing (unit, integration, Testcontainers) | 0 | – | – |
| Logging and observability | 0 | – | – |
| Docker and Compose | 1 | Phase 1 session | 2026-11-10 |
| Authentication and authorization | 0 | – | – |
| Next.js basics (later) | 0 | – | – |

Add rows when a new concept appears. Keep names specific.

## Exercises (done without AI)

- [ ] _Added by `cw-learn` per phase_

## Session log

Newest first. Written by `cw-learn` after each session.

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
