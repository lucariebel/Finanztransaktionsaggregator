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

    public List<Transaction> getTransactionsByCategorie(string categorie)
    {
        return _transactionRepository.GetTransactionsByCategorie(categorie).ToList();
        
    }
}