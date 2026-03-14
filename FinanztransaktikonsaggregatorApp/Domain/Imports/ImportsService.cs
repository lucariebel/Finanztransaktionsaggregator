using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public class ImportsService : IImportsService
{
    private readonly ITransactionService _transactionService;

    public ImportsService(ITransactionService transactionService)
    {
        _transactionService = transactionService;

    }
    public List<Transaction> ImportTransactions(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var transactions = new List<Transaction>();
        foreach (var line in lines.Skip(1))
        {
            var parts = line.Split(',');
            var transaction = new Transaction
            {
                Date = DateTime.Parse(parts[0]),
                Amount = ParserHelper.ParseDecimal(parts[1]),
                Description = parts[2],
                AccountNumber = ParserHelper.ParseInteger(parts[3])
            };
            transactions.Add(transaction);
            _transactionService.AddTransaction(transaction);
        }
        return transactions;
    }
}