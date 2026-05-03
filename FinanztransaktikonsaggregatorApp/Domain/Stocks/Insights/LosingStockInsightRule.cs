using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;

public class LosingStockInsightRule : IStockInsightRule
{
    public StockInsight? Evaluate(StockAnalyticsResult analyticsResult, List<Stock> stocks)
    {
        if (analyticsResult.WorstPerformingStock is null)
        {
            return null;
        }

        var profitOrLoss = CalculateProfitOrLoss(analyticsResult.WorstPerformingStock);

        if (profitOrLoss >= 0m)
        {
            return null;
        }

        return new StockInsight
        {
            Severity = StockInsightSeverity.Info,
            Title = "Worst performing stock",
            Message = $"{analyticsResult.WorstPerformingStock.TickerSymbol} is currently the weakest position with {profitOrLoss:C2} profit/loss."
        };
    }

    private static decimal CalculateProfitOrLoss(Stock stock)
    {
        var currentPrice = stock.LastKnownPrice ?? stock.AverageBuyPrice;
        var currentValue = stock.Quantity * currentPrice;
        var investedAmount = stock.Quantity * stock.AverageBuyPrice;

        return currentValue - investedAmount;
    }
}