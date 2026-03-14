using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    decimal GetTransactionsBycategory(string category);
    void AddTransaction(Transaction transaction);
}