using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    List<Transaction> GetAll();
    List<Transaction> GetTopExpenses(int count);
    decimal GetUsedBudgetByCategory(string category);
    decimal GetIncomeByCategory(string category);
    void AddTransaction(Transaction transaction);
}