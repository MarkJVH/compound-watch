# src/Api

## What
ASP.NET Core minimal API: the HTTP entry point of CompoundWatch. Translates HTTP requests into calls on Domain and Infrastructure and returns DTOs.

## Current state
- `Program.cs`: composition only; OpenAPI (development only), `GET /health`, `app.MapWatchlistEndpoints()`.
- `WatchlistEndpoints.cs`: `POST /watchlist` (201, 400, 404, 409) and `GET /watchlist` (200, 400). The current user comes from the `X-User-Id` header until auth exists. Request/response records live in the Api project.
- Runs on `http://localhost:5264` (see `Properties/launchSettings.json`).

## Standards
- References `Domain` and `Infrastructure`; nothing references `Api`.
- Group endpoints per feature with `MapGroup` in their own static class; keep `Program.cs` to composition only.
- Validate input once at the boundary; return `ProblemDetails` for errors.
- Never expose EF entities; use DTOs.
- Only map a unique-violation (`PostgresException` with `SqlState` 23505) to 409; let other `DbUpdateException`s surface as 500.
- Pass `CancellationToken` into every handler and downstream call.
- See the `dotnet-standards` skill for the full checklist.

## Run and test
```bash
docker compose up -d
dotnet run --project src/Api
curl -i http://localhost:5264/health
```
