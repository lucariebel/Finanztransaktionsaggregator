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
    public void Execute()
    {
        bool exportTransacion = true;

        MenuHelper.CreateHeader("Export DATA");
        Console.WriteLine();
        while (exportTransacion)
        {
            Console.WriteLine("You want to Export Transacions? \nPress y (Yes) Enter (No)");
            var key = Console.ReadKey(true).Key;
            Console.WriteLine();

            if (key == ConsoleKey.Y)
            {
                string filepath = InputHelper.GetRequiredString("Please enter the file path:");
                Console.WriteLine("Start Export...");
                string resultMessage = _exportService.ExportTransactions(filepath);
                Console.WriteLine(resultMessage);
                Console.WriteLine($"Export finished. File saved at {filepath}. Press Enter to return.");
                Console.ReadKey();
                exportTransacion = false;
            }
            else if (key == ConsoleKey.Enter)
            {
                exportTransacion = false;
            }
            else
            {
                Console.WriteLine("Invalid key!");
                Console.WriteLine();
            }
        }
    }
}