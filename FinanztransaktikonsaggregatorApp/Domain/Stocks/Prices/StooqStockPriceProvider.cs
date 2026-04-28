using System.Globalization;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;

public class StooqStockPriceProvider : IStockPriceProvider
{
    private readonly HttpClient _httpClient;

    public StooqStockPriceProvider()
    {
        _httpClient = new HttpClient();
    }

    public StockPriceQuote GetPrice(string tickerSymbol)
    {
        var normalizedTickerSymbol = NormalizeTickerSymbol(tickerSymbol);
        var requestUrl = BuildRequestUrl(normalizedTickerSymbol);
        var csvContent = _httpClient.GetStringAsync(requestUrl).GetAwaiter().GetResult();

        return ParseCsvResponse(normalizedTickerSymbol, csvContent);
    }

    private static string NormalizeTickerSymbol(string tickerSymbol)
    {
        var trimmedTickerSymbol = tickerSymbol.Trim().ToLowerInvariant();

        if (trimmedTickerSymbol.Contains("."))
        {
            return trimmedTickerSymbol;
        }

        return $"{trimmedTickerSymbol}.us";
    }

    private static string BuildRequestUrl(string normalizedTickerSymbol)
    {
        return $"https://stooq.com/q/l/?s={normalizedTickerSymbol}&f=sd2t2ohlcv&h&e=csv";
    }

    private static StockPriceQuote ParseCsvResponse(string tickerSymbol, string csvContent)
    {
        var lines = csvContent
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (lines.Length < 2)
        {
            throw new InvalidOperationException($"No price data was returned for ticker '{tickerSymbol}'.");
        }

        var values = lines[1].Split(',');

        if (values.Length < 7)
        {
            throw new InvalidOperationException($"Invalid price data was returned for ticker '{tickerSymbol}'.");
        }

        var closeValue = values[6];

        if (string.Equals(closeValue, "N/D", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"No close price is available for ticker '{tickerSymbol}'.");
        }

        var price = decimal.Parse(closeValue, CultureInfo.InvariantCulture);
        var timestamp = DateTime.Now;

        return new StockPriceQuote
        {
            TickerSymbol = tickerSymbol.ToUpperInvariant(),
            Price = price,
            Timestamp = timestamp
        };
    }
}