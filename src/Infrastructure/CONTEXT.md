# src/Infrastructure

## What
Implementations of everything that touches the outside world: PostgreSQL via EF Core (DbContext, mappings, migrations), RabbitMQ publishing, market-data clients and mail.

## Current state
Empty; the EF Core `DbContext` and first migration arrive in phase 2.

## Standards
- References only `Domain`. `Api` and `Worker` reference this project and register services through an `AddInfrastructure(...)` extension.
- Entity mapping in `IEntityTypeConfiguration<T>` classes; define keys, uniqueness, required, max lengths and foreign keys in the database too.
- One migration per logical change with a descriptive name; never edit an applied migration.
- `AsNoTracking()` for read-only queries; project to DTOs; avoid N+1.
- Integration tests use real Postgres (Testcontainers), not the in-memory provider.
- Configuration through the options pattern with validation on start.
- See the `dotnet-standards` skill.

## Local database
`docker compose up -d postgres` (user/password/database in `docker-compose.yml`, port 5432).
