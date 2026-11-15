using CompoundWatch.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompoundWatch.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<PriceQuote> PriceQuotes => Set<PriceQuote>();
    public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
