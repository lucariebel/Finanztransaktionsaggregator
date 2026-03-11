using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Transactions;

public interface ITransactionService
{
    decimal getTransactionsBycategory(string category);
}