using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Domain.Stocks;

public class NegativePortfolioInsightRuleTest
{
    [Fact]
    public void Evaluate_ReturnsInsightForNegativePortfolio()
    {
        var rule = new NegativePortfolioInsightRule();

        var analyticsResult = new StockAnalyticsResult
        {
            TotalProfitOrLoss = -25m
        };

        var insight = rule.Evaluate(analyticsResult, new List<Stock>());

        Assert.NotNull(insight);
        Assert.Equal(StockInsightSeverity.Warning, insight!.Severity);
        Assert.Equal("Negative portfolio performance", insight.Title);
    }
}