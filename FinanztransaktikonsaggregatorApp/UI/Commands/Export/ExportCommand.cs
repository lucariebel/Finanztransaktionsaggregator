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

    private void Export()
    {
        string filepath = InputHelper.GetRequiredString("Please enter the file path:");
        Console.WriteLine("Start Export...");
        string resultMessage = _exportService.ExportTransactions(filepath);
        Console.WriteLine(resultMessage);
        Console.WriteLine($"Export finished. File saved at {filepath}. ");
        Console.WriteLine("Press Enter to return.");
        Console.ReadKey();
    }

    public void Execute()
    {
        MenuHelper.CreateHeader("Export DATA");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("You want to export Transactions?", 
            () => Export());
    }
}