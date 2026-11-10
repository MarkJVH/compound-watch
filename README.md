# CompoundWatch

Leerproject: aandelentracker met watchlist, statistieken en koersalerts.
Stack: ASP.NET Core (.NET 10), PostgreSQL, RabbitMQ, Next.js.

## Lokaal draaien

```bash
docker compose up -d          # Postgres, RabbitMQ (UI :15672), Mailpit (UI :8025)
dotnet run --project src/Api  # GET /health
```

## Structuur

- `src/Api` – REST API
- `src/Worker` – achtergrondjobs en consumers
- `src/Domain` – entiteiten en domeinlogica
- `src/Infrastructure` – database en externe integraties
- `web/` – Next.js frontend (komt later)

## Bekende build-waarschuwing

`NU1903` op `Microsoft.OpenApi` 2.0.0 komt transitief uit `Microsoft.AspNetCore.OpenApi` (template). Versie 3.x is niet compatibel met ASP.NET Core 10. Oplossen zodra Microsoft.AspNetCore.OpenApi een gepatchte afhankelijkheid meebrengt.

Geen beleggingsadvies.
