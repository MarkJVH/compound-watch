namespace CompoundWatch.Domain.Tests;

public class InstrumentTests
{
    [Fact]
    public void Create_WithValidInput_GeneratesAnInstrument()
    {
        var instrument = Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", "USD");
        Assert.Equal("GOOGL", instrument.Symbol);
        Assert.Equal("Google Inc.", instrument.Name);
        Assert.Equal("NASDAQ", instrument.Exchange);
        Assert.Equal("USD", instrument.Currency);
    }

    [Fact]
    public void Create_WithValidInput_GeneratesNonEmptyId()
    {
        var instrument = Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", "USD");
        Assert.NotEqual(Guid.Empty, instrument.Id);
    }

    [Fact]
    public void Create_WithValidInput_GeneratesUniqueId()
    {
        var instrument = Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", "USD");
        var instrument2 = Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", "USD");

        Assert.NotEqual(instrument.Id, instrument2.Id);
    }

    [Theory]
    [InlineData(" googl")]
    [InlineData("   GooGL ")]
    [InlineData("GoogL")]
    public void Create_WithMessySymbol_NormalizesSymbol(string symbol)
    {
        var instrument = Instrument.Create(symbol, "Google Inc.", "NASDAQ", "USD");
        Assert.Equal("GOOGL", instrument.Symbol);
    }

    [Theory]
    [InlineData(" usd")]
    [InlineData("Usd ")]
    [InlineData("usd")]
    public void Create_WithMessyCurrency_NormalizesCurrency(string currency)
    {
        var instrument = Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", currency);
        Assert.Equal("USD", instrument.Currency);
    }

    [Fact]
    public void Create_WithPaddedLowercaseExchange_NormalizesExchange()
    {
        var instrument = Instrument.Create("GOOGL", "Google Inc.", " nasdaq ", "USD");

        Assert.Equal("NASDAQ", instrument.Exchange);
    }

    [Fact]
    public void Create_WithPaddedName_TrimsName()
    {
        var instrument = Instrument.Create("GOOGL", "  Google Inc. ", "NASDAQ", "USD");

        Assert.Equal("Google Inc.", instrument.Name);
    }

    [Theory]
    [InlineData("", "Google Inc.", "NASDAQ", "USD")]
    [InlineData("GOOGL", "", "NASDAQ", "USD")]
    [InlineData("GOOGL", "Google Inc.", "", "USD")]
    [InlineData("GOOGL", "Google Inc.", "NASDAQ", "")]
    [InlineData("   ", "Google Inc.", "NASDAQ", "USD")]
    [InlineData("GOOGL", "   ", "NASDAQ", "USD")]
    [InlineData("GOOGL", "Google Inc.", "   ", "USD")]
    [InlineData("GOOGL", "Google Inc.", "NASDAQ", "   ")]
    [InlineData(null, "Google Inc.", "NASDAQ", "USD")]
    [InlineData("GOOGL", null, "NASDAQ", "USD")]
    [InlineData("GOOGL", "Google Inc.", null, "USD")]
    [InlineData("GOOGL", "Google Inc.", "NASDAQ", null)]
    public void Create_WithEmptyInput_Throws(
        string? symbol,
        string? name,
        string? exchange,
        string? currency
    )
    {
        Assert.ThrowsAny<ArgumentException>(() =>
            Instrument.Create(symbol!, name!, exchange!, currency!)
        );
    }

    [Theory]
    [InlineData("E1R")]
    [InlineData("EURO")]
    [InlineData("EU")]
    [InlineData("E R")]
    [InlineData("€€€")]
    [InlineData("ÉÉÉ")]
    public void Create_WithWrongCurrency_Throws(string currency)
    {
        Assert.Throws<ArgumentException>(() =>
            Instrument.Create("GOOGL", "Google Inc.", "NASDAQ", currency)
        );
    }
}
