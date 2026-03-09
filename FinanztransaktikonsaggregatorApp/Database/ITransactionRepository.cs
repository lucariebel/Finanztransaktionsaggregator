using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface ITransactionRepository
{
    List<Transaction> GetAll();
    Transaction Insert(Transaction transaction);
    List<Transaction> GetTransactionsByMonth(int year, int month);
    Transaction Update(Transaction transaction);
    void Delete(Transaction transaction);
}