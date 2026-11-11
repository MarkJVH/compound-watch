using CompoundWatch.Domain;

namespace CompoundWatch.Domain.Tests;

public class WatchlistItemTests
{
    [Fact]
    public void Create_WithValidInput_SetsProperties()
    {
        var instrumentId = Guid.CreateVersion7();
        var addedAt = new DateTimeOffset(2026, 1, 5, 10, 0, 0, TimeSpan.FromHours(1));

        var item = WatchlistItem.Create(instrumentId, addedAt);

        Assert.NotEqual(Guid.Empty, item.Id);
        Assert.Equal(instrumentId, item.InstrumentId);
        Assert.Equal(addedAt, item.AddedAt);
    }

    [Fact]
    public void Create_WithEmptyInstrumentId_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            WatchlistItem.Create(Guid.Empty, DateTimeOffset.UtcNow)
        );
    }

    [Fact]
    public void Create_WithNonUtcTime_StoresAsUtc()
    {
        var local = new DateTimeOffset(2026, 1, 5, 10, 0, 0, TimeSpan.FromHours(5));

        var item = WatchlistItem.Create(Guid.CreateVersion7(), local);

        Assert.Equal(TimeSpan.Zero, item.AddedAt.Offset);
    }
}
