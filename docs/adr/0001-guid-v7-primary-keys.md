# 0001. Guid v7 primary keys

Status: Accepted
Date: 2026-11-13

## Context
Entities need a primary key that can be generated in the domain (before saving), without a database round trip. The app is single-user for now but may get more users, so keys should not be guessable or collide across systems.

## Decision
`Instrument` and `WatchlistItem` use `Guid.CreateVersion7()` as primary key, generated in the `Create` factory method. `PriceQuote` has no surrogate key; its key is the composite `(InstrumentId, Date)`.

## Alternatives considered
- `int` identity: the id only exists after the insert, and ids are predictable.
- Guid v4 (random): fragments the B-tree index on inserts.
- ULID: needs an extra dependency; v7 is built into .NET.

## Consequences
- Ids are time-ordered, so inserts stay index-friendly.
- The id exists before persistence, which simplifies tests and relationships.
- Keys take 16 bytes instead of 4.
- The creation time is embedded in the id (leaks the timestamp; acceptable here).
