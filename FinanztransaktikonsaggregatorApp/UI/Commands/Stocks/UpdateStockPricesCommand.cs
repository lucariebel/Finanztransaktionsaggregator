using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public class UpdateStockPricesCommand : ICommand
{
    private readonly IStockService _stockService;

    public UpdateStockPricesCommand(IStockService stockService)
    {
        _stockService = stockService;
    }

    public string Name { get; } = "Update Prices";

    public void Execute()
    {
        MenuHelper.CreateHeader("UPDATE STOCK PRICES");
        Console.WriteLine();

        _stockService.UpdatePrices();

        Console.WriteLine("Stock prices successfully updated.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}