using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class StockInsightService : IStockInsightService
{
    private readonly IStockService _stockService;
    private readonly IStockAnalyticsService _stockAnalyticsService;
    private readonly List<IStockInsightRule> _insightRules;

    public StockInsightService(IStockService stockService, IStockAnalyticsService stockAnalyticsService)
    {
        _stockService = stockService;
        _stockAnalyticsService = stockAnalyticsService;
        _insightRules = CreateDefaultRules();
    }

    public List<StockInsight> GenerateInsights()
    {
        var stocks = _stockService.GetAll();
        var analyticsResult = _stockAnalyticsService.AnalyzePortfolio();
        var insights = new List<StockInsight>();

        if (stocks.Count == 0)
        {
            return insights;
        }

        foreach (var rule in _insightRules)
        {
            var insight = rule.Evaluate(analyticsResult, stocks);

            if (insight is not null)
            {
                insights.Add(insight);
            }
        }

        return insights;
    }

    private static List<IStockInsightRule> CreateDefaultRules()
    {
        return new List<IStockInsightRule>
        {
            new PositivePortfolioInsightRule(),
            new NegativePortfolioInsightRule(),
            new HighConcentrationInsightRule(),
            new LosingStockInsightRule(),
            new StrongPriceDropInsightRule()
        };
    }
}