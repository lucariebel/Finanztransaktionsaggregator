using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Accounts.History;

public interface IAccountBalanceHistoryService
{
    AccountBalanceHistory GetHistory(int accountId, DateTime? startDate = null, DateTime? endDate = null);
    decimal GetBalanceAt(int accountId, DateTime date);
}
