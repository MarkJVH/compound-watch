# src/Domain

## What
The core of the application: entities, value objects, domain rules and message contracts. Pure C#.

## Current state
`Instrument`, `WatchlistItem` and `PriceQuote` (daily OHLC + volume, composite key `(InstrumentId, Date)` set up in Infrastructure). Alert rules come later.

## Standards
- See ADR 0001 (Guid v7 keys) and ADR 0002 (rich, persistence-ignorant model).
- `PriceQuote` only enforces `high >= low` and positive prices; open/close are deliberately not bound to `[low, high]` because provider data can deviate.
- `ArgumentOutOfRangeException.ThrowIf…` throws `ArgumentOutOfRangeException`; pass `nameof(param)` as paramName when throwing manually.
- Use `= null!` only for properties EF fills through the private constructor.
- **No dependencies** on other projects, EF Core, ASP.NET or messaging libraries.
- Persistence-ignorant: no EF attributes; mapping lives in `Infrastructure`.
- Enforce invariants in the entity or value object (private setters, factory methods), not in callers.
- Money and prices: `decimal` plus an explicit currency; never `double`.
- Time via `TimeProvider` passed in; store UTC.
- Unit tests here need no infrastructure and must be fast.
