using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IExportService
{
    string ExportTransactionsAsCSV(string filepath, TransactionsFilter transactionFilter);
    string ExportTransactionsAsPDF(string filepath, TransactionsFilter transactionFilter);
}