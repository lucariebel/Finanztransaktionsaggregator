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

    public decimal getTransactionsByCategorie(string categorie)
    {
        List<Transaction> transactionsSPendings = _transactionRepository.GetTransactionsSpendingsByCategory(categorie).ToList();
        decimal usedBudget = transactionsSPendings.Sum(t => t.Amount);
        return usedBudget;
        
    }
}