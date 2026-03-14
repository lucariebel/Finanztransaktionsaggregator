using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ImportDataCommand : ICommand
{
    private readonly IImportsService _importsService;
    public string Name { get; } = "Import Data (CSV)";
    public ImportDataCommand(IImportsService importsService)
    {
        _importsService = importsService;
    }

    public void Execute()
    {
        string filepath = InputHelper.GetRequiredString("Please enter the correct file path:");
        filepath = InputHelper.GetExistingFilePath($"Your file does not exist, try again!",filepath);
        List<Transaction> transactions = _importsService.ImportTransactions(filepath);
        Console.WriteLine("Start Import...");
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("Finished Import. Press Enter to return"); 
        
        Console.ReadKey();
    }
}