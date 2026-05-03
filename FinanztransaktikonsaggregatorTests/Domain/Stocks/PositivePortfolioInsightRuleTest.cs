using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Domain.Stocks;

public class PositivePortfolioInsightRuleTest
{
    [Fact]
    public void Evaluate_ReturnsInsightForPositivePortfolio()
    {
        var rule = new PositivePortfolioInsightRule();

        var analyticsResult = new StockAnalyticsResult
        {
            TotalProfitOrLoss = 50m
        };

        var insight = rule.Evaluate(analyticsResult, new List<Stock>());

        Assert.NotNull(insight);
        Assert.Equal(StockInsightSeverity.Info, insight!.Severity);
        Assert.Equal("Positive portfolio performance", insight.Title);
    }
}