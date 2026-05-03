using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorTests.Mocks;

class FakeTransactionService : ITransactionService
{
    private readonly List<Transaction> _transactions = new();

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public List<Transaction> GetAll()
    {
        return _transactions.ToList();
    }
    public List<Transaction> GetByAccountId(int accountId)
    {
        return _transactions
            .Where(t => t.AccountNumber == accountId)
            .ToList();
    }
    public List<Transaction> GetTopExpenses(int count)
    {
        return _transactions
            .Where(t => t.Amount < 0)
            .OrderBy(t => t.Amount)
            .Take(count)
            .ToList();
    }

    public decimal GetUsedBudgetByCategoryAndMonth(string category, int year, int month)
    {
        return _transactions
            .Where(t => t.Category == category
                        && t.Date.Year == year
                        && t.Date.Month == month)
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));
    }

    public decimal GetIncomeByCategory(string category, int year, int month)
    {
        return _transactions
            .Where(t => t.Category == category
                        && t.Date.Year == year
                        && t.Date.Month == month)
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);
    }

    public List<Transaction> GetFilteredTransactions(TransactionsFilter transactionFilter)
    {
        List<Transaction> filteredTransactions = _transactions.ToList();

        if (transactionFilter.AccountNumbers != null && transactionFilter.AccountNumbers.Any())
            filteredTransactions = filteredTransactions
                .Where(t => transactionFilter.AccountNumbers.Contains(t.AccountNumber))
                .ToList();

        if (transactionFilter.Categories != null && transactionFilter.Categories.Any())
            filteredTransactions = filteredTransactions
                .Where(t => transactionFilter.Categories.Contains(t.Category))
                .ToList();

        if (transactionFilter.StartDate.HasValue)
            filteredTransactions = filteredTransactions
                .Where(t => t.Date >= transactionFilter.StartDate.Value)
                .ToList();

        if (transactionFilter.EndDate.HasValue)
            filteredTransactions = filteredTransactions
                .Where(t => t.Date <= transactionFilter.EndDate.Value)
                .ToList();

        if (!transactionFilter.StartDate.HasValue && !transactionFilter.EndDate.HasValue)
        {
            if (transactionFilter.Year.HasValue)
                filteredTransactions = filteredTransactions
                    .Where(t => t.Date.Year == transactionFilter.Year.Value)
                    .ToList();

            if (transactionFilter.Month.HasValue)
                filteredTransactions = filteredTransactions
                    .Where(t => t.Date.Month == transactionFilter.Month.Value)
                    .ToList();

            if (transactionFilter.Day.HasValue)
                filteredTransactions = filteredTransactions
                    .Where(t => t.Date.Day == transactionFilter.Day.Value)
                    .ToList();
        }

        if (transactionFilter.MinAmount.HasValue)
            filteredTransactions = filteredTransactions
                .Where(t => t.Amount >= transactionFilter.MinAmount.Value)
                .ToList();

        if (transactionFilter.MaxAmount.HasValue)
            filteredTransactions = filteredTransactions
                .Where(t => t.Amount <= transactionFilter.MaxAmount.Value)
                .ToList();

        if (!string.IsNullOrWhiteSpace(transactionFilter.DescriptionContains))
            filteredTransactions = filteredTransactions
                .Where(t => t.Description != null &&
                            t.Description.Contains(transactionFilter.DescriptionContains,
                                StringComparison.OrdinalIgnoreCase))
                .ToList();

        return filteredTransactions;
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
