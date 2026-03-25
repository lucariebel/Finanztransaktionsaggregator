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
}