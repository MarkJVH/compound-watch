namespace CompoundWatch.Domain;

public class WatchlistItem
{
    public Guid Id { get; private set; }
    public Guid InstrumentId { get; private set; }
    public DateTimeOffset AddedAt { get; private set; }

    private WatchlistItem() { }

    public static WatchlistItem Create(Guid instrumentId, DateTimeOffset addedAt)
    {
        if (instrumentId == Guid.Empty)
            throw new ArgumentException("InstrumentId is required", nameof(instrumentId));

        return new WatchlistItem
        {
            Id = Guid.CreateVersion7(),
            InstrumentId = instrumentId,
            AddedAt = addedAt.ToUniversalTime(),
        };
    }
}
