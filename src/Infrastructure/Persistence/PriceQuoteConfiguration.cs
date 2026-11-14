using CompoundWatch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompoundWatch.Infrastructure.Persistence;

public class PriceQuoteConfiguration : IEntityTypeConfiguration<PriceQuote>
{
    public void Configure(EntityTypeBuilder<PriceQuote> builder)
    {
        builder.HasKey(x => new { x.InstrumentId, x.Date });
        builder
            .HasOne<Instrument>()
            .WithMany()
            .HasForeignKey(x => x.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.Close).IsRequired().HasPrecision(18, 6);
        builder.Property(x => x.Open).IsRequired().HasPrecision(18, 6);
        builder.Property(x => x.High).IsRequired().HasPrecision(18, 6);
        builder.Property(x => x.Low).IsRequired().HasPrecision(18, 6);
    }
}
