using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

public class TransactionCommand : ICommand
{
    private readonly ITransactionService _transactionService;

    public TransactionCommand(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public string Name { get; } = "Transactions";

    public void Execute()
    {
        Console.Clear();
        MenuHelper.CreateHeader("SEARCH TRANSACTIONS");
        Console.WriteLine();
        var filter = InputHelper.ReadFilter();
        var transactions = _transactionService.GetFilteredTransactions(filter);

        Console.WriteLine();
        PrintTransactions(transactions);
        PrintSummary(transactions);

        Console.WriteLine();
        Console.WriteLine("Press any key to return.");
        Console.ReadKey();
    }

    private void PrintTransactions(List<Transaction> transactions)
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("No Transactions found.");
            return;
        }

        Console.WriteLine($"Found transactions: {transactions.Count}");
        MenuHelper.CreateHorizontalLine();

        Console.WriteLine(
            "{0,-12} | {1,12} | {2,-18} | {3,-15} | {4}",
            "Date",
            "Amount",
            "Category",
            "Account",
            "Description");

        MenuHelper.CreateHorizontalLine();

        foreach (var transaction in transactions)
        {
            Console.ForegroundColor = transaction.Amount >= 0
                ? ConsoleColor.Green
                : ConsoleColor.Red;

            Console.WriteLine(
                "{0,-12} | {1,12:C2} | {2,-18} | {3,-15} | {4}",
                transaction.Date.ToString("yyyy-MM-dd"),
                transaction.Amount,
                transaction.Category,
                transaction.AccountNumber,
                transaction.Description);

            Console.ResetColor();
        }
    }

    private void PrintSummary(List<Transaction> transactions)
    {
        if (transactions.Count == 0)
            return;

        var income = transactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        var expenses = transactions
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));

        var balance = income - expenses;

        Console.WriteLine();
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine("[ SUMMARY ]");
        Console.WriteLine($"Income:   {income,12:C2}");
        Console.WriteLine($"Expenses: {expenses,12:C2}");
        Console.WriteLine($"Balance:  {balance,12:C2}");
    }
}
