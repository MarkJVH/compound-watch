# src/Worker

## What
.NET Worker Service for background work: scheduled jobs (for example fetching prices) and message consumers (RabbitMQ), such as evaluating price alerts and sending mail.

## Current state
- `Program.cs` registers a placeholder `Worker` hosted service that only logs. Replace it with real jobs and consumers.

## Standards
- References `Domain` and `Infrastructure`; nothing references `Worker`.
- One `BackgroundService` or consumer per responsibility; honour the `stoppingToken` for graceful shutdown.
- Consumers must be idempotent (at-least-once delivery), ack/nack explicitly and dead-letter poison messages.
- Outbound HTTP via `IHttpClientFactory` with timeout and resilience; respect provider rate limits.
- Use `TimeProvider`, not `DateTime.UtcNow`.
- See the `dotnet-standards` skill.

## Run
```bash
docker compose up -d
dotnet run --project src/Worker
```
Mail sent in development appears in Mailpit (http://localhost:8025).
