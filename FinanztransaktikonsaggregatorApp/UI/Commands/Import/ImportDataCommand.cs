using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Import;

public class ImportDataCommand : ICommand
{
    private readonly IImportsService _importsService;
    public string Name { get; } = "Import Data (CSV)";
    public ImportDataCommand(IImportsService importsService)
    {
        _importsService = importsService;
    }

    private List<Transaction> AskUserForCategory(List<Transaction> uncategorized)
    {
        List<Transaction> categorizedTransactions = new List<Transaction>();
        Console.WriteLine("You have some uncategorized data. You can enter the categories here.");
        foreach (var transaction in uncategorized)
        {
            Console.WriteLine($"Description: {transaction.Description}");
            Console.ForegroundColor = transaction.Amount >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Amount: {transaction.Amount}");
            Console.ResetColor();

            Console.WriteLine("Enter a category, or press Enter to skip:");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Skipped.");
                continue; 
            }

            transaction.Category = input; 
            categorizedTransactions.Add( transaction );
        }
        return categorizedTransactions;
    }

    private void PrintTransactions(List<Transaction> transactionList)
    {
        foreach (Transaction transaction in transactionList)
        {
            Console.ForegroundColor = transaction.Amount >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(transaction);
            Console.ResetColor();
        }
    }
    private void Import()
    {
        string filepath = InputHelper.GetRequiredString("Please enter the correct file path:");
        filepath = InputHelper.GetExistingFilePath($"Your file does not exist, try again!", filepath);
        var transactions = _importsService.ImportTransactions(filepath);
        Console.WriteLine("Start Import...");
        PrintTransactions(transactions.AllTransactions);
        var categorized = AskUserForCategory(transactions.UncategorizedTransactions);
        List<Transaction> mergedList = _importsService.MergeList(categorized, transactions.AllTransactions);
        PrintTransactions(mergedList);
        var resultMessage = _importsService.SaveTransacitons(mergedList);
        Console.WriteLine(resultMessage);
        Console.WriteLine("Finished Import. Press Enter to return");
        Console.ReadKey();
    }
    public void Execute()
    {
        MenuHelper.CreateHeader("IMPORT DATA");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("Do you want to import budgets?",
            () => Import());
    }
}