namespace FinanztransaktikonsaggregatorApp.Entities;

public class AccountBalanceHistory
{
    public Account Account { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}
