using CompoundWatch.Domain;
using CompoundWatch.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompoundWatch.Api;

public static class WatchlistEndpoints
{
    public static IEndpointRouteBuilder MapWatchlistEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/watchlist");
        group.MapPost(
            "/",
            async (
                AddWatchlistItemRequest body,
                [FromHeader(Name = "X-User-Id")] Guid userId,
                AppDbContext db,
                CancellationToken ct
            ) =>
            {
                if (body.InstrumentId == Guid.Empty || userId == Guid.Empty)
                {
                    return Results.BadRequest();
                }

                var exists = await db.Instruments.AnyAsync(i => i.Id == body.InstrumentId, ct);

                if (!exists)
                {
                    return Results.NotFound();
                }

                var item = WatchlistItem.Create(body.InstrumentId, userId, DateTimeOffset.UtcNow);
                db.WatchlistItems.Add(item);

                try
                {
                    await db.SaveChangesAsync(ct);
                }
                catch (DbUpdateException)
                {
                    return Results.Conflict();
                }

                return Results.Created(
                    $"/watchlist/{item.Id}",
                    new WatchlistItemResponse(item.Id, item.InstrumentId, item.AddedAt)
                );
            }
        );

        group.MapGet(
            "/",
            async (
                [FromHeader(Name = "X-User-Id")] Guid userId,
                AppDbContext db,
                CancellationToken ct
            ) =>
            {
                var items = await db
                    .WatchlistItems.AsNoTracking()
                    .Where(w => w.UserId == userId)
                    .Select(w => new WatchlistItemResponse(w.Id, w.InstrumentId, w.AddedAt))
                    .ToListAsync();
                return Results.Ok(items);
            }
        );
        return app;
    }
}

public record AddWatchlistItemRequest(Guid InstrumentId);

public record WatchlistItemResponse(Guid Id, Guid InstrumentId, DateTimeOffset AddedAt);
