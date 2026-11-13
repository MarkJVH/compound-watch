# 0002. Rich domain model, persistence-ignorant Domain

Status: Accepted
Date: 2026-11-13

## Context
The domain must stay valid regardless of who creates or changes an entity (API, worker, tests). EF Core is the persistence layer but must not shape the domain.

## Decision
- Entities have private setters and are created through a static `Create` factory that enforces the invariants.
- A private parameterless constructor exists only for EF Core; properties it fills use `= null!`.
- `src/Domain` has no EF Core reference or attributes. Mapping lives in `Infrastructure` (`IEntityTypeConfiguration<T>`).
- Uniqueness and foreign keys are enforced by the database (constraints), not by the domain; the domain only validates a single object.

## Alternatives considered
- Anemic model with validation in services: invariants can be bypassed and get duplicated.
- EF attributes on entities: couples the domain to EF Core.

## Consequences
- Invariants live in one place and are unit-testable without a database.
- Entities cannot be changed without an explicit method.
- More boilerplate (factory, private constructor, separate mapping classes).
- Duplicate keys surface as database exceptions on save and must be translated by the caller.
