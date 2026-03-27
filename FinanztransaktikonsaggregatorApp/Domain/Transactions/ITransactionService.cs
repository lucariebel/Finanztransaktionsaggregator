using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    List<Transaction> GetAll();
    List<Transaction> GetTopExpenses(int count);
    decimal GetUsedBudgetByCategoryAndMonth(string category, int year, int month);
    decimal GetIncomeByCategory(string category, int year, int month);
    void AddTransaction(Transaction transaction);
    List<Transaction> GetFilteredTransactions(TransactionFilter transactionFilter);
}