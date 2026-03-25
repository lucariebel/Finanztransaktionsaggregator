using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Database;

namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public class TransactionService : ITransactionService
{

    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public List<Transaction> GetAll() => _transactionRepository.GetAll();

    public List<Transaction> GetTopExpenses(int count)
    {
        return _transactionRepository.GetAll()
            .Where(t => t.Amount < 0)
            .OrderBy(t => t.Amount)
            .Take(count)
            .ToList();
    }

    public decimal GetUsedBudgetByCategory(string category)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionsSpendingsByCategory(category).ToList();
        decimal usedBudget = transactionsSPendings
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));
        return usedBudget;
    }

    public decimal GetIncomeByCategory(string category)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionsSpendingsByCategory(category).ToList();
        decimal incomeBudget = transactionsSPendings
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);
        return incomeBudget;
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactionRepository.Insert(transaction);
    }
}