using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class ShowStocksCommand : ICommand
{
    private readonly IStockService _stockService;

    public ShowStocksCommand(IStockService stockService)
    {
        _stockService = stockService;
    }

    public string Name { get; } = "Show Stocks";

    public void Execute()
    {
        MenuHelper.CreateHeader("STOCKS OVERVIEW");
        Console.WriteLine();

        var stocks = _stockService.GetAll();

        if (stocks.Count == 0)
        {
            Console.WriteLine("No stocks have been defined.");
        }
        else
        {
            Console.WriteLine(
                "{0,-6} | {1,-10} | {2,-25} | {3,12} | {4,15} | {5,15} | {6,15}",
                "ID",
                "Ticker",
                "Name",
                "Quantity",
                "Buy Price",
                "Last Price",
                "Profit/Loss"
            );

            MenuHelper.CreateHorizontalLine();

            foreach (var stockItem in stocks)
            {
                Console.WriteLine(
                    "{0,-6} | {1,-10} | {2,-25} | {3,12:N2} | {4,15:C2} | {5,15:C2} | {6,15:C2}",
                    stockItem.Id,
                    stockItem.TickerSymbol,
                    stockItem.Name,
                    stockItem.Quantity,
                    stockItem.AverageBuyPrice,
                    stockItem.LastKnownPrice ?? stockItem.AverageBuyPrice,
                    stockItem.GetProfitOrLoss()
                );
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}