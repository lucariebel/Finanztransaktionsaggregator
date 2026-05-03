using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ExportCommand : ICommand
{

    private readonly IExportService _exportService;
    public string Name { get; } = "Export";
    public ExportCommand(IExportService exportService)
    {
        _exportService = exportService;
    }

    private void Export(Func<string, TransactionsFilter, string> exportFunc, string fileFormat)
    {
        string filepath = InputHelper.GetRequiredString("Please enter the file path and name (example: for windows:C:\\Users\\user\\OneDrive\\Desktop\\example for linux:/home/user/Desktop) :");

        var filter = InputHelper.ReadFilter();
        
        Console.WriteLine("Start Export...");
        string resultMessage = exportFunc(filepath, filter);
        Console.WriteLine(resultMessage);
        Console.WriteLine($"Export finished. File saved at {filepath}.{fileFormat} ");
        Console.WriteLine("Press Enter to return.");
        Console.ReadKey();
    }

    private void ExportTransactions()
    {
        Console.WriteLine("Would you like to export the transactions as a .csv or a .pdf file?");

        string fileFormat;

        while (true)
        {
            fileFormat = InputHelper
                .GetRequiredString("Please enter the file format as csv or pdf:")
                .ToLower();

            if (fileFormat == "pdf" || fileFormat == "csv")
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter 'csv' or 'pdf'.");
        }

        if (fileFormat == "pdf")
        {
            Export(_exportService.ExportTransactionsAsPDF, fileFormat);
        }
        else
        {
            Export(_exportService.ExportTransactionsAsCSV, fileFormat);
        }
    }

    public void Execute()
    {
        MenuHelper.CreateHeader("Export DATA");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("You want to export Transactions?", 
            () => ExportTransactions());
    }
}