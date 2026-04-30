using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Import;

public class ImportResult
{
    public List<Transaction> AllTransactions { get; set; } = new();
    public List<Transaction> UncategorizedTransactions { get; set; } = new();

    public List<Transaction> MissingAccountNumberTransactions { get; set; } = new();
}

