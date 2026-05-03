using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class UpdateStockCommand : ICommand
{
    private readonly IStockService _stockService;

    public UpdateStockCommand(IStockService stockService)
    {
        _stockService = stockService;
    }

    public string Name { get; } = "Update Stock";

    public void Execute()
    {
        MenuHelper.CreateHeader("UPDATE STOCK");
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
        Console.WriteLine("Please enter the ID you want to update.");
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

        var existingStock = _stockService.GetById(id);

        if (existingStock is null)
        {
            Console.WriteLine("Stock not found.");
            Console.ReadKey();
            return;
        }

        var tickerSymbol = InputHelper.GetRequiredString("Please enter the new ticker symbol:");
        var name = InputHelper.GetRequiredString("Please enter the new stock name:");
        var quantity = InputHelper.GetRequiredDecimal("Please enter the new quantity:");
        var averageBuyPrice = InputHelper.GetRequiredDecimal("Please enter the new average buy price:");

        existingStock.TickerSymbol = tickerSymbol;
        existingStock.Name = name;
        existingStock.Quantity = quantity;
        existingStock.AverageBuyPrice = averageBuyPrice;

        var updatedStock = _stockService.Update(existingStock);

        Console.WriteLine();
        Console.WriteLine($"Stock successfully updated: ID {updatedStock.Id}, Ticker {updatedStock.TickerSymbol}, Quantity {updatedStock.Quantity:N2}");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}