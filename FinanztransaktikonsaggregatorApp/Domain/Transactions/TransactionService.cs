using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public class TransactionService : ITransactionService
{

    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public List<Transaction> GetAll() => _transactionRepository.GetAll();

    public List<Transaction> GetByAccountId(int accountId) => _transactionRepository.GetByAccountId(accountId);

    public List<Transaction> GetTransactionsByMonth(int year, int month)
    {
        return _transactionRepository.GetTransactionsByMonth(year, month);
    }

    public List<Transaction> GetTopExpensesByMonth(int count, int year, int month)
    {
        return _transactionRepository.GetTransactionsByMonth(year, month)
            .Where(t => t.Amount < 0)
            .OrderBy(t => t.Amount)
            .Take(count)
            .ToList();
    }

    public decimal GetUsedBudgetByCategoryAndMonth(string category, int year, int month)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionSpendingsByCategoryAndMonth(category, year, month).ToList();
        decimal usedBudget = transactionsSPendings
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));
        return usedBudget;
    }

    public decimal GetIncomeByCategory(string category, int year, int month)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionSpendingsByCategoryAndMonth(category,year, month).ToList();
        decimal incomeBudget = transactionsSPendings
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);
        return incomeBudget;
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactionRepository.Insert(transaction);
    }
    public List<Transaction> GetFilteredTransactions(TransactionsFilter transactionFilter)
    {
        return _transactionRepository.GetFiltered(transactionFilter);
    }

    public List<Transaction> SortTransactions(List<Transaction> transactions, TransactionSortFilter sortOptions)
    {
        if (sortOptions.SortBy == TransactionSortBy.None)
            return transactions;

        return sortOptions.SortBy switch
        {
            TransactionSortBy.Date => sortOptions.Descending
                ? transactions.OrderByDescending(t => t.Date).ToList()
                : transactions.OrderBy(t => t.Date).ToList(),

            TransactionSortBy.Amount => sortOptions.Descending
                ? transactions.OrderByDescending(t => t.Amount).ToList()
                : transactions.OrderBy(t => t.Amount).ToList(),

            TransactionSortBy.Category => sortOptions.Descending
                ? transactions.OrderByDescending(t => t.Category).ToList()
                : transactions.OrderBy(t => t.Category).ToList(),

            TransactionSortBy.Account => sortOptions.Descending
                ? transactions.OrderByDescending(t => t.AccountNumber).ToList()
                : transactions.OrderBy(t => t.AccountNumber).ToList(),

            _ => transactions
        };
    }

}
