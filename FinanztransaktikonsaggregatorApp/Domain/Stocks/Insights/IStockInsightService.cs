namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public interface IStockInsightService
{
    List<StockInsight> GenerateInsights();
}