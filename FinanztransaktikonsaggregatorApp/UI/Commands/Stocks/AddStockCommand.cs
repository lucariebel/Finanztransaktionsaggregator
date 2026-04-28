using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class AddStockCommand : ICommand
{
    private readonly IStockService _stockService;

    public AddStockCommand(IStockService stockService)
    {
        _stockService = stockService;
    }

    public string Name { get; } = "Add Stock";

    public void Execute()
    {
        MenuHelper.CreateHeader("ADD STOCK");
        Console.WriteLine();

        var tickerSymbol = InputHelper.GetRequiredString("Please enter the ticker symbol:");
        var name = InputHelper.GetRequiredString("Please enter the stock name:");
        var quantity = InputHelper.GetRequiredDecimal("Please enter the quantity:");
        var averageBuyPrice = InputHelper.GetRequiredDecimal("Please enter the average buy price:");

        var stock = new Stock
        {
            TickerSymbol = tickerSymbol,
            Name = name,
            Quantity = quantity,
            AverageBuyPrice = averageBuyPrice,
            LastKnownPrice = averageBuyPrice,
            LastUpdated = DateTime.Now
        };

        var createdStock = _stockService.Insert(stock);

        Console.WriteLine();
        Console.WriteLine($"Stock successfully added: ID {createdStock.Id}, Ticker {createdStock.TickerSymbol}, Quantity {createdStock.Quantity:N2}");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}