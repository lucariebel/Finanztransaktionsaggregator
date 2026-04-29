using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;

public class StockAnalyticsResult

{

    public decimal TotalInvestedAmount { get; set; }

    public decimal TotalCurrentValue { get; set; }

    public decimal TotalProfitOrLoss { get; set; }

    public decimal TotalPerformancePercentage { get; set; }

    public int StockCount { get; set; }

    public int WinningStockCount { get; set; }

    public int LosingStockCount { get; set; }

    public Stock? BestPerformingStock { get; set; }

    public Stock? WorstPerformingStock { get; set; }

    public Stock? LargestPosition { get; set; }

}