using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorTests.Mocks;

namespace FinanztransaktikonsaggregatorTests.Domain.Stocks;

public class StockAnalyticsServiceTest
{
    [Fact]
    public void AnalyzePortfolio_CalculatesTotalValuesAndProfit()
    {
        var stocks = new List<Stock>
        {
            new Stock
            {
                Id = 1,
                TickerSymbol = "AAPL",
                Name = "Apple Inc.",
                Quantity = 2m,
                AverageBuyPrice = 100m,
                LastKnownPrice = 150m
            },
            new Stock
            {
                Id = 2,
                TickerSymbol = "MSFT",
                Name = "Microsoft",
                Quantity = 1m,
                AverageBuyPrice = 200m,
                LastKnownPrice = 180m
            }
        };

        var repository = new FakeStockRepository(stocks);
        var stockService = new StockService(repository, new FixedStockPriceProvider(0m));
        var analyticsService = new StockAnalyticsService(stockService);

        var result = analyticsService.AnalyzePortfolio();

        Assert.Equal(400m, result.TotalInvestedAmount);
        Assert.Equal(480m, result.TotalCurrentValue);
        Assert.Equal(80m, result.TotalProfitOrLoss);
        Assert.Equal(20m, result.TotalPerformancePercentage);
        Assert.Equal("AAPL", result.BestPerformingStock!.TickerSymbol);
        Assert.Equal("MSFT", result.WorstPerformingStock!.TickerSymbol);
    }
}