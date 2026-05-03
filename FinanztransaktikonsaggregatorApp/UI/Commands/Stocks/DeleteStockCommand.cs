using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class DeleteStockCommand : ICommand
{
    private readonly IStockService _stockService;

    public DeleteStockCommand(IStockService stockService)
    {
        _stockService = stockService;
    }

    public string Name { get; } = "Delete Stock";

    public void Execute()
    {
        MenuHelper.CreateHeader("DELETE STOCK");
        Console.WriteLine();

        var stocks = _stockService.GetAll();

        if (stocks.Count == 0)
        {
            Console.WriteLine("No stocks have been defined.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

        StockConsolePrinter.PrintSelectionTable(stocks);

        Console.WriteLine();
        Console.WriteLine("Please enter the ID you want to delete.");
        Console.WriteLine("Press Enter to cancel.");

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return;
        }

        var id = InputHelper.GetValidId(
            "Invalid ID! Please enter a valid stock ID:",
            stockId => _stockService.GetById(stockId) is not null,
            input
        );

        var stock = _stockService.GetById(id);

        if (stock is null)
        {
            Console.WriteLine("Stock not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Do you really want to delete stock '{stock.TickerSymbol}'?");
        Console.WriteLine("Press Y to delete or Enter to cancel.");

        var key = Console.ReadKey(true).Key;

        if (key != ConsoleKey.Y)
        {
            return;
        }

        _stockService.Delete(stock);

        Console.WriteLine();
        Console.WriteLine("Stock successfully deleted.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}