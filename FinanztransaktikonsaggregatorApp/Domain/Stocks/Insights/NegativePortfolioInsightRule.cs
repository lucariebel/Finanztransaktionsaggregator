using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class NegativePortfolioInsightRule : IStockInsightRule
{
    public StockInsight? Evaluate(StockAnalyticsResult analyticsResult, List<Stock> stocks)
    {
        if (analyticsResult.TotalProfitOrLoss >= 0m)
        {
            return null;
        }

        return new StockInsight
        {
            Severity = StockInsightSeverity.Warning,
            Title = "Negative portfolio performance",
            Message = $"The portfolio is currently below the invested amount by {analyticsResult.TotalProfitOrLoss:C2}."
        };
    }
}