using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Analytics;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Insights;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class StockMenuCommand : ICommand
{
    private readonly IStockService _stockService;
    private readonly IStockAnalyticsService _stockAnalyticsService;
    private readonly IStockInsightService _stockInsightService;

    public StockMenuCommand(
        IStockService stockService,
        IStockAnalyticsService stockAnalyticsService, 
        IStockInsightService stockInsightService)
    {
        _stockService = stockService;
        _stockAnalyticsService = stockAnalyticsService;
        _stockInsightService = stockInsightService;
    }

    public string Name { get; } = "Stocks";

    public void Execute()
    {
        var stockCommands = new List<ICommand>
        {
            new ShowStocksCommand(_stockService),
            new AddStockCommand(_stockService),
            new UpdateStockCommand(_stockService),
            new DeleteStockCommand(_stockService),
            new UpdateStockPricesCommand(_stockService),
            new ShowStockAnalyticsCommand(_stockAnalyticsService),
            new ShowStockInsightsCommand(_stockInsightService)
        };

        var stockMenu = new MenuController("STOCK MANAGEMENT", stockCommands);
        stockMenu.Run();
    }
}