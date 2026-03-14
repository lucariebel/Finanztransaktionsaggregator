using FinanztransaktikonsaggregatorApp.Entities;
namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IImportsService
{
    List<Transaction> ImportTransactions(string filePath);
}