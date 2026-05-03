using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Domain.Stocks;

public class HighConcentrationInsightRuleTest
{
    [Fact]
    public void Evaluate_ReturnsWarningForLargePosition()
    {
        var largestStock = new Stock
        {
            Id = 1,
            TickerSymbol = "AAPL",
            Name = "Apple Inc.",
            Quantity = 10m,
            AverageBuyPrice = 100m,
            LastKnownPrice = 100m
        };

        var analyticsResult = new StockAnalyticsResult
        {
            TotalCurrentValue = 1500m,
            LargestPosition = largestStock
        };

        var rule = new HighConcentrationInsightRule();

        var insight = rule.Evaluate(analyticsResult, new List<Stock> { largestStock });

        Assert.NotNull(insight);
        Assert.Equal(StockInsightSeverity.Warning, insight!.Severity);
        Assert.Equal("High portfolio concentration", insight.Title);
    }
}