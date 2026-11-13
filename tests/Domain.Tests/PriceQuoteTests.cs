namespace CompoundWatch.Domain.Tests;

public class PriceQuoteTests
{
    private static PriceQuote Create(
        Guid? instrumentId = null,
        DateOnly? date = null,
        decimal open = 10,
        decimal high = 12,
        decimal low = 9,
        decimal close = 11,
        long volume = 1000
    ) =>
        PriceQuote.Create(
            instrumentId ?? Guid.NewGuid(),
            date ?? new DateOnly(2026, 11, 12),
            open,
            high,
            low,
            close,
            volume
        );

    [Fact]
    public void Create_WithValidInput_GeneratesPriceQuote()
    {
        Guid guid = Guid.NewGuid();
        DateOnly date = new(2026, 11, 13);
        PriceQuote pq = Create(guid, date);
        Assert.Equal(guid, pq.InstrumentId);
        Assert.Equal(date, pq.Date);

        Assert.Equal(10, pq.Open);
        Assert.Equal(12, pq.High);
        Assert.Equal(9, pq.Low);
        Assert.Equal(11, pq.Close);
        Assert.Equal(1000, pq.Volume);
    }

    [Fact]
    public void Create_WithEmptyDate_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Create(date: DateOnly.MinValue));
        Assert.Equal("date", ex.ParamName);
    }

    [Fact]
    public void Create_WithEmptyInstrumentId_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Create(instrumentId: Guid.Empty));
        Assert.Equal("instrumentId", ex.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_WithNonPositiveOpen_Throws(decimal open)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create(open: open));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_WithNonPositiveHigh_Throws(decimal high)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Create(high: high));
        Assert.Equal("high", ex.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_WithNonPositiveLow_Throws(decimal low)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Create(low: low));
        Assert.Equal("low", ex.ParamName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_WithNonPositiveClose_Throws(decimal close)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Create(close: close));
        Assert.Equal("close", ex.ParamName);
    }

    [Fact]
    public void Create_WithNegativeVolume_Throws()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Create(volume: -1));
        Assert.Equal("volume", ex.ParamName);
    }

    [Fact]
    public void Create_WithZeroVolume_Succeeds()
    {
        Assert.Equal(0, Create(volume: 0).Volume);
    }

    [Fact]
    public void Create_WithHighLowerThanLow_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Create(high: 1, low: 2));
        Assert.Equal("high", ex.ParamName);
    }

    [Fact]
    public void Create_WithHighEqualToLow_Succeeds()
    {
        var pq = Create(high: 10, low: 10);
        Assert.Equal(10, pq.High);
        Assert.Equal(10, pq.Low);
    }
}
