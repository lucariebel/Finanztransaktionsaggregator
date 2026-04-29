using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class ShowStockAnalyticsCommand : ICommand
{
    private readonly IStockAnalyticsService _stockAnalyticsService;

    public ShowStockAnalyticsCommand(IStockAnalyticsService stockAnalyticsService)
    {
        _stockAnalyticsService = stockAnalyticsService;
    }

    public string Name { get; } = "Portfolio Analytics";

    public void Execute()
    {
        MenuHelper.CreateHeader("PORTFOLIO ANALYTICS");
        Console.WriteLine();

        var result = _stockAnalyticsService.AnalyzePortfolio();

        if (result.StockCount == 0)
        {
            Console.WriteLine("No stocks have been defined.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Stocks in portfolio:      {result.StockCount}");
        Console.WriteLine($"Winning stocks:           {result.WinningStockCount}");
        Console.WriteLine($"Losing stocks:            {result.LosingStockCount}");
        Console.WriteLine();

        Console.WriteLine($"Total invested amount:    {result.TotalInvestedAmount:C2}");
        Console.WriteLine($"Total current value:      {result.TotalCurrentValue:C2}");
        Console.WriteLine($"Total profit/loss:        {result.TotalProfitOrLoss:C2}");
        Console.WriteLine($"Total performance:        {result.TotalPerformancePercentage:N2} %");
        Console.WriteLine();

        PrintStockHighlight("Best performing stock", result.BestPerformingStock);
        PrintStockHighlight("Worst performing stock", result.WorstPerformingStock);
        PrintStockHighlight("Largest position", result.LargestPosition);

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void PrintStockHighlight(string label, Stock? stock)
    {
        if (stock is null)
        {
            Console.WriteLine($"{label}: -");
            return;
        }

        var currentPrice = stock.LastKnownPrice ?? stock.AverageBuyPrice;
        var currentValue = stock.Quantity * currentPrice;
        var investedAmount = stock.Quantity * stock.AverageBuyPrice;
        var profitOrLoss = currentValue - investedAmount;

        Console.WriteLine($"{label}:");
        Console.WriteLine($"  {stock.TickerSymbol} - {stock.Name}");
        Console.WriteLine($"  Current value: {currentValue:C2}");
        Console.WriteLine($"  Profit/Loss:   {profitOrLoss:C2}");
        Console.WriteLine();
    }
}