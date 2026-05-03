using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class ShowStockInsightsCommand : ICommand
{
    private readonly IStockInsightService _stockInsightService;

    public ShowStockInsightsCommand(IStockInsightService stockInsightService)
    {
        _stockInsightService = stockInsightService;
    }

    public string Name { get; } = "Portfolio Insights";

    public void Execute()
    {
        MenuHelper.CreateHeader("PORTFOLIO INSIGHTS");
        Console.WriteLine();

        var insights = _stockInsightService.GenerateInsights();

        if (insights.Count == 0)
        {
            Console.WriteLine("No insights are available for the current portfolio.");
            Console.WriteLine();
            Console.WriteLine("This can happen when no stocks exist or no relevant rule was triggered.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        foreach (var insight in insights)
        {
            PrintInsight(insight);
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void PrintInsight(StockInsight insight)
    {
        Console.WriteLine($"[{FormatSeverity(insight.Severity)}] {insight.Title}");
        Console.WriteLine(insight.Message);
    }

    private static string FormatSeverity(StockInsightSeverity severity)
    {
        return severity switch
        {
            StockInsightSeverity.Info => "INFO",
            StockInsightSeverity.Warning => "WARNING",
            StockInsightSeverity.Critical => "CRITICAL",
            _ => "INFO"
        };
    }
}