using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Dashboard;

public interface IDashboardService
{
    decimal GetTotalNetWorth();
    Dictionary<Account, decimal> GetBalancesPerAccount();
    MonthlyDashboardSummary GetMonthlySummary(int year, int month);
}