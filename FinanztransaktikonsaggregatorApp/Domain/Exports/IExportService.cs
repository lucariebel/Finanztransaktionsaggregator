using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;
namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IExportService
{
    string ExportTransactionsAsCSV(string filepath);
    string ExportTransactionsAsPDF(string filepath);
}