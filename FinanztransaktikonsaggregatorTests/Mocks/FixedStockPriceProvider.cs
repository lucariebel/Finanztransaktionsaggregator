using FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;

namespace FinanztransaktikonsaggregatorTests.Mocks;

class FixedStockPriceProvider : IStockPriceProvider
{
    private readonly decimal _price;

    public FixedStockPriceProvider(decimal price)
    {
        _price = price;
    }

    public StockPriceQuote GetPrice(string tickerSymbol)
    {
        return new StockPriceQuote
        {
            TickerSymbol = tickerSymbol,
            Price = _price,
            Timestamp = new DateTime(2026, 1, 2)
        };
    }
}