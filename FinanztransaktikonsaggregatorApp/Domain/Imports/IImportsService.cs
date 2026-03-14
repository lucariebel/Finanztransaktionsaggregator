using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;
namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IImportsService
{
    ImportResult ImportTransactions(string filePath);

    string SaveTransacitons(List<Transaction> transactions);

    List<Transaction> MergeList(List<Transaction> categorized ,List<Transaction> transactions);
}