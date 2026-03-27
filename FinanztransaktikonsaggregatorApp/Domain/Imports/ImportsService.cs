using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Category;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;
using System.Globalization;

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
            var parts = line.Split(';');
            var transaction = new Transaction
            {
                Date = DateTime.ParseExact(
                    parts[0],
                    new[] { "dd.MM.yyyy", "yyyy-MM-dd", "MM/dd/yyyy" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                ),
                Amount = ParserHelper.ParseDecimal(parts[1]),
                Description = parts[2],
                Category = _categoryService.GetCategoryForDescription(parts[3]),
                AccountNumber = ParserHelper.ParseRequiredId(parts[4])
            };
            transactions.Add(transaction);
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