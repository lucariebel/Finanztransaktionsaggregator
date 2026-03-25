using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using System.Text;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public class ExportService : IExportService
{
    private readonly ITransactionService _transactionService;

    public ExportService(ITransactionService transactionService)
    {
        _transactionService = transactionService;

    }

    public string ExportTransactions(string filepath)
    {
        List<Transaction> transactions =_transactionService.GetAll();
        using (var writer = new StreamWriter(filepath, false, Encoding.UTF8))
        {
            writer.WriteLine("Date, Amount, Description, Category, AccountNumber");
            foreach(var transaction in transactions)
            {
                writer.WriteLine($"{transaction.Date},{transaction.Amount},{transaction.Description},{transaction.Category},{transaction.AccountNumber}");
            }
        }
        return "Succesfully exported";
    }
}