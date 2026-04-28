namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;

public interface IStockPriceProvider
{
    StockPriceQuote GetPrice(string tickerSymbol);
}