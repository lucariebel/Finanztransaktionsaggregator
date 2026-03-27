using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IExportService
{
    string ExportTransactionsAsCSV(string filepath, TransactionFilter transactionFilter);
    string ExportTransactionsAsPDF(string filepath, TransactionFilter transactionFilter);
}