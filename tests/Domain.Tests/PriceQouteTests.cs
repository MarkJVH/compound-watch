using CompoundWatch.Domain;

namespace CompoundWatch.Domain.Tests;

public class PriceQuoteTests
{
    [Fact]
    public void Create_WithValidInput_GeneratesPriceQuote()
    {
        Guid instrumentId = Guid.NewGuid();
        DateOnly date = new DateOnly(2026, 11, 12);
        decimal open = new decimal(1);
        decimal high = new decimal(1);
        decimal low = new decimal(1);
        decimal close = new decimal(1);
        long volume = new long(new Int64(10000));
        var pq = PriceQuote.Create(instrumentId, date, open, high, low, close, volume);
        Assert.Equal(instrumentId, pq.InstrumentId);
        Assert.Equal(date, pq.Date);

        Assert.Equal(open, pq.Open);
        Assert.Equal(high, pq.High);
        Assert.Equal(low, pq.Low);
        Assert.Equal(close, pq.Close);
        Assert.Equal(volume, pq.Volume);
    }
}
