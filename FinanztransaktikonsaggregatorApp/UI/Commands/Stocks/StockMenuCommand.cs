using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class StockMenuCommand : ICommand
{
    private readonly IStockService _stockService;

    public StockMenuCommand(IStockService stockService)
    {
        _stockService = stockService;
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
            new UpdateStockPricesCommand(_stockService)
        };

        var stockMenu = new MenuController("STOCK MANAGEMENT", stockCommands);
        stockMenu.Run();
    }
}