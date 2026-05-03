using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

public static class StockConsolePrinter
{
    public static void PrintSelectionTable(IEnumerable<Stock> stocks)
    {
        Console.WriteLine(
            "{0,-6} | {1,-10} | {2,-25} | {3,12} | {4,15}",
            "ID",
            "Ticker",
            "Name",
            "Quantity",
            "Buy Price"
        );

        MenuHelper.CreateHorizontalLine();

        foreach (var stock in stocks)
        {
            Console.WriteLine(
                "{0,-6} | {1,-10} | {2,-25} | {3,12:N2} | {4,15:C2}",
                stock.Id,
                stock.TickerSymbol,
                stock.Name,
                stock.Quantity,
                stock.AverageBuyPrice
            );
        }
    }
}