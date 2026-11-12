namespace CompoundWatch.Domain;

public class PriceQuote
{
    public Guid InstrumentId { get; private set; }
    public DateOnly Date { get; private set; }

    public decimal Open { get; private set; }
    public decimal High { get; private set; }
    public decimal Low { get; private set; }
    public decimal Close { get; private set; }
    public long Volume { get; private set; }

    private PriceQuote() { }

    public static PriceQuote Create(
        Guid instrumentId,
        DateOnly date,
        decimal open,
        decimal high,
        decimal low,
        decimal close,
        long volume
    )
    {
        if (instrumentId == Guid.Empty)
            throw new ArgumentException("InstrumentId must be set.", nameof(instrumentId));
        if (date == default)
            throw new ArgumentException("Date must be set.", nameof(date));

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(open);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(high);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(low);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(close);
        ArgumentOutOfRangeException.ThrowIfNegative(volume);

        if (high < low)
            throw new ArgumentException("High must be >= low.", nameof(high));

        return new PriceQuote
        {
            InstrumentId = instrumentId,
            Open = open,
            High = high,
            Low = low,
            Close = close,
            Date = date,
            Volume = volume,
        };
    }
}
