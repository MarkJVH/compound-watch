# src/Domain

## What
The core of the application: entities, value objects, domain rules and message contracts. Pure C#.

## Current state
Empty; entities arrive in phase 2 (instruments, watchlist, alert rules).

## Standards
- **No dependencies** on other projects, EF Core, ASP.NET or messaging libraries.
- Persistence-ignorant: no EF attributes; mapping lives in `Infrastructure`.
- Enforce invariants in the entity or value object (private setters, factory methods), not in callers.
- Money and prices: `decimal` plus an explicit currency; never `double`.
- Time via `TimeProvider` passed in; store UTC.
- Unit tests here need no infrastructure and must be fast.
