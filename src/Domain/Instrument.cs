namespace CompoundWatch.Domain;

public class Instrument
{
    public Guid Id { get; private set; }
    public string Symbol { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Exchange { get; private set; } = null!;
    public string Currency { get; private set; } = null!;

    private Instrument() { }

    public static Instrument Create(string symbol, string name, string exchange, string currency)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        ArgumentException.ThrowIfNullOrWhiteSpace(exchange);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(symbol);

        var localCurrency = currency.ToUpperInvariant().Trim();
        if (localCurrency.Length != 3)
            throw new ArgumentException("Currency should be 3 characters");
        if (!localCurrency.All(char.IsAsciiLetter))
            throw new ArgumentException("Only characters allowed for currency");

        return new Instrument
        {
            Currency = localCurrency,
            Exchange = exchange.ToUpperInvariant().Trim(),
            Name = name.Trim(),
            Symbol = symbol.ToUpperInvariant().Trim(),
            Id = Guid.CreateVersion7(),
        };
    }
}
