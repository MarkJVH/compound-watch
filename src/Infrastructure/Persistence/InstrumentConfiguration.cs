using CompoundWatch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompoundWatch.Infrastructure.Persistence;

public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Symbol, x.Exchange }).IsUnique();
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Symbol).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Exchange).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Currency).IsRequired().HasMaxLength(3);
    }
}
