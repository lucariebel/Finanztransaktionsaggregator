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
        AskUserForMissingAccountNumbers(transactions);
        PrintTransactions(transactions.AllTransactions);
        var categorized = AskUserForCategory(transactions.UncategorizedTransactions);
        List<Transaction> mergedList = _importsService.MergeList(categorized, transactions.AllTransactions);
        PrintTransactions(mergedList);
        var resultMessage = _importsService.SaveTransacitons(mergedList);
        Console.WriteLine(resultMessage);
        Console.WriteLine("Finished Import. Press Enter to return");
        Console.ReadKey();
    }
    private void AskUserForMissingAccountNumbers(ImportResult importResult)
    {
        var missing = importResult.MissingAccountNumberTransactions;

        if (!missing.Any())
            return;

        if (missing.Count == importResult.AllTransactions.Count)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("In der gesamten CSV fehlt die Account Number.");
            Console.ResetColor();
            var accountNumber = InputHelper.AskForAccountNumber("Bitte Account Number für die gesamte CSV eingeben:");

            foreach (var transaction in importResult.AllTransactions)
            {
                transaction.AccountNumber = accountNumber;
            }

            return;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Einige Transaktionen haben keine Account Number.");
        Console.ResetColor();
        foreach (var transaction in missing)
        {
            Console.WriteLine();
            Console.WriteLine($"Beschreibung: {transaction.Description}");
            Console.WriteLine($"Betrag: {transaction.Amount}");
            Console.WriteLine($"Datum: {transaction.Date:dd.MM.yyyy}");

            transaction.AccountNumber = InputHelper.AskForAccountNumber("Bitte Account Number für diese Transaktion eingeben:");
        }
    }
    public void Execute()
    {
        MenuHelper.CreateHeader("IMPORT DATA");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("Do you want to import budgets?",
            () => Import());
    }
}