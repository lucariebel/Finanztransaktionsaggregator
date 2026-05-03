using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public interface IStockInsightRule
{
    StockInsight? Evaluate(StockAnalyticsResult analyticsResult, List<Stock> stocks);
}