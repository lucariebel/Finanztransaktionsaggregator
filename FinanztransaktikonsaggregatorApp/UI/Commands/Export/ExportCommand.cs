using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Imports;

namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ExportCommand : ICommand
{

    private readonly IExportService _exportService;
    public string Name { get; } = "Export";
    public ExportCommand(IExportService exportService)
    {
        _exportService = exportService;
    }

    private void Export(Func<string, string> exportFunc)
    {
        string filepath = InputHelper.GetRequiredString("Please enter the file path:");
        Console.WriteLine("Start Export...");
        string resultMessage = exportFunc(filepath);
        Console.WriteLine(resultMessage);
        Console.WriteLine($"Export finished. File saved at {filepath}. ");
        Console.WriteLine("Press Enter to return.");
        Console.ReadKey();
    }

    private void ExportTransactions()
    {
        Console.WriteLine("Would you like to export the transactions as a .csv or a .pdf file?");
        string fileFormat = InputHelper.GetRequiredString("Please enter the file format:");
        if(fileFormat == ".pdf")
        {
            Export(_exportService.ExportTransactionsAsPDF);
        }
        else
        {
            Export(_exportService.ExportTransactionsAsCSV);
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