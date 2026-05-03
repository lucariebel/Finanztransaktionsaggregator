using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class HighConcentrationInsightRule : IStockInsightRule
{
    private const decimal ConcentrationThresholdPercentage = 50m;

    public StockInsight? Evaluate(StockAnalyticsResult analyticsResult, List<Stock> stocks)
    {
        if (analyticsResult.LargestPosition is null)
        {
            return null;
        }

        if (analyticsResult.TotalCurrentValue == 0m)
        {
            return null;
        }

        var largestPositionValue = CalculateCurrentValue(analyticsResult.LargestPosition);
        var concentrationPercentage = largestPositionValue / analyticsResult.TotalCurrentValue * 100m;

        if (concentrationPercentage < ConcentrationThresholdPercentage)
        {
            return null;
        }

        return new StockInsight
        {
            Severity = StockInsightSeverity.Warning,
            Title = "High portfolio concentration",
            Message = $"{analyticsResult.LargestPosition.TickerSymbol} represents {concentrationPercentage:N2}% of the portfolio value."
        };
    }

    private static decimal CalculateCurrentValue(Stock stock)
    {
        var currentPrice = stock.LastKnownPrice ?? stock.AverageBuyPrice;
        return stock.Quantity * currentPrice;
    }
}