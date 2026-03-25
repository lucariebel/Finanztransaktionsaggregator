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

    public decimal GetUsedBudgetByCategory(string category)
    {
        return _transactions
            .Where(t => t.Category == category)
            .Sum(t => t.Amount);
    }
    public List<Transaction> GetAllTransactions() => _transactions;
}