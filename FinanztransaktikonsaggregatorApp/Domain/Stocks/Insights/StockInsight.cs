namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class StockInsight
{
    public StockInsightSeverity Severity { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }
}