namespace FinanztransaktikonsaggregatorApp.Domain.Dashboard;

public class MonthlyDashboardSummary
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal CashFlow => Income - Expenses;
    public int TransactionCount { get; set; }
}
