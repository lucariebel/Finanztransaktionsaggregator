using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    List<Transaction> GetAll();
    List<Transaction> GetByAccountId(int accountId);
    List<Transaction> GetTransactionsByMonth(int year, int month);
    List<Transaction> GetTopExpensesByMonth(int count, int year, int month);
    decimal GetUsedBudgetByCategoryAndMonth(string category, int year, int month);
    decimal GetIncomeByCategory(string category, int year, int month);
    void AddTransaction(Transaction transaction);
    List<Transaction> GetFilteredTransactions(TransactionsFilter transactionFilter);
    List<Transaction> SortTransactions(List<Transaction> transactions, TransactionSortFilter sortOptions);
}
