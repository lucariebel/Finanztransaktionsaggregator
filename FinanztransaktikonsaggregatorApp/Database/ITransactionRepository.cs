using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface ITransactionRepository
{
    List<Transaction> GetAll();
    List<Transaction> GetTransactionSpendingsByCategoryAndMonth(string category, int year, int month);
    Transaction Insert(Transaction transaction);
    List<Transaction> GetTransactionsByMonth(int year, int month);
    Transaction Update(Transaction transaction);
    void Delete(Transaction transaction);
    List<Transaction> GetFiltered(TransactionFilter transactionFilter);
}