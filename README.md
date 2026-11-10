# CompoundWatch

Learning project: stock tracker with watchlist, statistics and price alerts.
Stack: ASP.NET Core (.NET 10), PostgreSQL, RabbitMQ, Next.js.

## Run locally

```bash
docker compose up -d          # Postgres, RabbitMQ (UI :15672), Mailpit (UI :8025)
dotnet run --project src/Api  # GET /health
```

## Structure

- `src/Api` – REST API
- `src/Worker` – background jobs and consumers
- `src/Domain` – entities and domain logic
- `src/Infrastructure` – database and external integrations
- `web/` – Next.js frontend (later)

## Known build warning

`NU1903` on `Microsoft.OpenApi` 2.0.0 comes transitively from `Microsoft.AspNetCore.OpenApi` (template). Version 3.x is not compatible with ASP.NET Core 10. Resolve once Microsoft.AspNetCore.OpenApi ships a patched dependency.

Not investment advice.
