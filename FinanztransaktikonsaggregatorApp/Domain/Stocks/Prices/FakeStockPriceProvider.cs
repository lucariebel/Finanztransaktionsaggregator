namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;

public class FakeStockPriceProvider : IStockPriceProvider
{
    private readonly Random _random = new();

    public StockPriceQuote GetPrice(string tickerSymbol)
    {
        var price = 100 + (decimal)_random.NextDouble() * 100;

        return new StockPriceQuote
        {
            TickerSymbol = tickerSymbol,
            Price = Math.Round(price, 2),
            Timestamp = DateTime.Now
        };
    }
}