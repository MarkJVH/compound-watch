using CompoundWatch.Domain;
using CompoundWatch.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

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
                return Results.Created();
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
                return Results.Ok();
            }
        );
        return app;
    }
}

public record AddWatchlistItemRequest(Guid InstrumentId);
