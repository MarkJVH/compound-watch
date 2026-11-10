---
name: local-stack
description: Start, check and stop the local CompoundWatch environment (Postgres, RabbitMQ, Mailpit, API). Use for questions about running locally or failing services.
---

# Local stack

## Start
```bash
docker compose up -d
dotnet run --project src/Api
```

## Check
- `docker compose ps` – all services must be `running`/`healthy`
- `curl -i http://localhost:<port>/health` – expect HTTP 200 (port in `src/Api/Properties/launchSettings.json`)
- RabbitMQ UI: http://localhost:15672 (guest/guest)
- Mailpit UI: http://localhost:8025

## Stop
```bash
docker compose down        # keep data
docker compose down -v     # also wipe the Postgres volume
```

## Troubleshooting
- Port in use: check 5432, 5672, 15672, 1025, 8025.
- Postgres not healthy: `docker compose logs postgres`.
