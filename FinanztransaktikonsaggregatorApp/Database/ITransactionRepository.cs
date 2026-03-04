using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface ITransactionRepository
{
    List<Transaction> GetAll();
    void Insert(Transaction transaction);
}