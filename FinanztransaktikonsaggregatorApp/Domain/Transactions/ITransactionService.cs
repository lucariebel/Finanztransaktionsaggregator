using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    decimal GetUsedBudgetByCategoryAndMonth(string category, int year, int month);
    decimal GetIncomeByCategory(string category, int year, int month);
    void AddTransaction(Transaction transaction);
}