using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;

public class StockAnalyticsService : IStockAnalyticsService
{
    private readonly IStockService _stockService;

    public StockAnalyticsService(IStockService stockService)
    {
        _stockService = stockService;
    }

    public StockAnalyticsResult AnalyzePortfolio()
    {
        var stocks = _stockService.GetAll();

        var result = new StockAnalyticsResult
        {
            StockCount = stocks.Count
        };

        if (stocks.Count == 0)
        {
            return result;
        }

        result.TotalInvestedAmount = stocks.Sum(CalculateInvestedAmount);
        result.TotalCurrentValue = stocks.Sum(CalculateCurrentValue);
        result.TotalProfitOrLoss = result.TotalCurrentValue - result.TotalInvestedAmount;
        result.TotalPerformancePercentage = CalculatePerformancePercentage(
            result.TotalInvestedAmount,
            result.TotalProfitOrLoss
        );

        result.WinningStockCount = stocks.Count(stock => CalculateProfitOrLoss(stock) > 0m);
        result.LosingStockCount = stocks.Count(stock => CalculateProfitOrLoss(stock) < 0m);

        result.BestPerformingStock = stocks
            .OrderByDescending(CalculateProfitOrLoss)
            .FirstOrDefault();

        result.WorstPerformingStock = stocks
            .OrderBy(CalculateProfitOrLoss)
            .FirstOrDefault();

        result.LargestPosition = stocks
            .OrderByDescending(CalculateCurrentValue)
            .FirstOrDefault();

        return result;
    }

    private static decimal CalculateInvestedAmount(Stock stock)
    {
        return stock.Quantity * stock.AverageBuyPrice;
    }

    private static decimal CalculateCurrentValue(Stock stock)
    {
        var currentPrice = stock.LastKnownPrice ?? stock.AverageBuyPrice;
        return stock.Quantity * currentPrice;
    }

    private static decimal CalculateProfitOrLoss(Stock stock)
    {
        return CalculateCurrentValue(stock) - CalculateInvestedAmount(stock);
    }

    private static decimal CalculatePerformancePercentage(decimal investedAmount, decimal profitOrLoss)
    {
        if (investedAmount == 0m)
        {
            return 0m;
        }

        return profitOrLoss / investedAmount * 100m;
    }
}