using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    List<Transaction> getTransactionsByCategorie(string categorie);
}