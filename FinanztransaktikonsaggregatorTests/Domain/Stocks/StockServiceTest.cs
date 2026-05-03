using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorTests.Mocks;

namespace FinanztransaktikonsaggregatorTests.Domain.Stocks;

public class StockServiceTest
{
    [Fact]
    public void UpdatePrices_UpdatesCurrentAndPreviousPrice()
    {
        var stock = new Stock
        {
            Id = 1,
            TickerSymbol = "AAPL",
            Name = "Apple Inc.",
            Quantity = 2m,
            AverageBuyPrice = 100m,
            LastKnownPrice = 110m,
            LastUpdated = new DateTime(2026, 1, 1)
        };

        var repository = new FakeStockRepository(new List<Stock> { stock });
        var priceProvider = new FixedStockPriceProvider(125m);
        var service = new StockService(repository, priceProvider);

        service.UpdatePrices();

        Assert.Equal(110m, stock.PreviousKnownPrice);
        Assert.Equal(new DateTime(2026, 1, 1), stock.PreviousUpdated);
        Assert.Equal(125m, stock.LastKnownPrice);
        Assert.Equal(new DateTime(2026, 1, 2), stock.LastUpdated);
    }
}