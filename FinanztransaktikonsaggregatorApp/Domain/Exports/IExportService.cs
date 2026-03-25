using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;
namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public interface IExportService
{
    string ExportTransactions(string filepath);
}