using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;

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
}