using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Category;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public class ImportsService : IImportsService
{
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;

    public ImportsService(ITransactionService transactionService, ICategoryService categoryService)
    {
        _transactionService = transactionService;
        _categoryService = categoryService;

    }
    public ImportResult ImportTransactions(string filePath)
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
                Category = _categoryService.GetCategoryForDescription(parts[2]),
                AccountNumber = ParserHelper.ParseInteger(parts[3])
            };
            transactions.Add(transaction);
            //_transactionService.AddTransaction(transaction);
        }
        var uncategorized = transactions
        .Where(t => t.Category == "Uncategorized")
        .ToList();

        return new ImportResult
        {
            AllTransactions = transactions,
            UncategorizedTransactions = uncategorized
        };
    }

    public string SaveTransacitons(List<Transaction> transactions)
    {
        foreach(var transaction in transactions)
        {
            _transactionService.AddTransaction(transaction);
        }
        
        return "Saved succesfully";
    }

    public List<Transaction> MergeList(List<Transaction> categorized, List<Transaction> transactions)
    {
        foreach (var transaction in categorized)
        {
            var original = transactions.First(t => t == transaction);
            original.Category = transaction.Category;
        }
        return transactions;
    }
}