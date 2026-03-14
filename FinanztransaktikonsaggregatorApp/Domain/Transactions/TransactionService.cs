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

    public decimal GetTransactionsBycategory(string category)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionsSpendingsByCategory(category).ToList();
        decimal usedBudget = transactionsSPendings.Sum(t => t.Amount);
        return usedBudget;
        
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactionRepository.Insert(transaction);
    }
}