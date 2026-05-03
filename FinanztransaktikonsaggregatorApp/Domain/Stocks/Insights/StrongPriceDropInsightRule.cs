using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class StrongPriceDropInsightRule : IStockInsightRule
{
    private const decimal PriceDropThresholdPercentage = -5m;

    public StockInsight? Evaluate(StockAnalyticsResult analyticsResult, List<Stock> stocks)
    {
        var strongestDrop = stocks
            .Where(HasPreviousPrice)
            .OrderBy(CalculatePriceChangePercentage)
            .FirstOrDefault();

        if (strongestDrop is null)
        {
            return null;
        }

        var priceChangePercentage = CalculatePriceChangePercentage(strongestDrop);

        if (priceChangePercentage > PriceDropThresholdPercentage)
        {
            return null;
        }

        return new StockInsight
        {
            Severity = StockInsightSeverity.Critical,
            Title = "Strong price drop since last update",
            Message = $"{strongestDrop.TickerSymbol} dropped by {priceChangePercentage:N2}% since the previous price update."
        };
    }

    private static bool HasPreviousPrice(Stock stock)
    {
        return stock.LastKnownPrice.HasValue
               && stock.PreviousKnownPrice.HasValue
               && stock.PreviousKnownPrice.Value != 0m;
    }

    private static decimal CalculatePriceChangePercentage(Stock stock)
    {
        return (stock.LastKnownPrice!.Value - stock.PreviousKnownPrice!.Value)
               / stock.PreviousKnownPrice.Value
               * 100m;
    }
}