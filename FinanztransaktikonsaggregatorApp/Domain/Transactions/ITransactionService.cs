using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    decimal GetUsedBudgetByCategory(string category);
    decimal GetIncomeByCategory(string category);
    void AddTransaction(Transaction transaction);
}