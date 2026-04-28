namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;

public class StockPriceQuote
{
    public string TickerSymbol { get; set; }
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
}