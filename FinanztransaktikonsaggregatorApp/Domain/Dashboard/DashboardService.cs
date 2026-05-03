using FinanztransaktikonsaggregatorApp.Domain.Accounts;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;

    public DashboardService(ITransactionService transactionService, IAccountService accountService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
    }

    public decimal GetTotalNetWorth()
    {
        var totalInitialBalances = _accountService.GetAll().Sum(a => a.InitialBalance);

        var totalTransactionSum = _transactionService.GetAll().Sum(t => t.Amount);

        return totalInitialBalances + totalTransactionSum;
    }

    public Dictionary<Account, decimal> GetBalancesPerAccount()
    {
        var allTransactions = _transactionService.GetAll();
        var allAccounts = _accountService.GetAll();

        var balances = new Dictionary<Account, decimal>();

        var transactionSums = allTransactions
            .GroupBy(t => t.AccountNumber)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(t => t.Amount)
            );

        foreach (var account in allAccounts)
        {
            var currentBalance = account.InitialBalance;

            if (transactionSums.TryGetValue(account.Id, out var sumOfTransactions)) currentBalance += sumOfTransactions;

            balances.Add(account, currentBalance);
        }

        return balances;
    }

    public MonthlyDashboardSummary GetMonthlySummary(int year, int month)
    {
        var transactions = _transactionService.GetTransactionsByMonth(year, month);

        var expenses = transactions
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));

        var income = transactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        return new MonthlyDashboardSummary
        {
            Year = year,
            Month = month,
            Income = income,
            Expenses = expenses,
            TransactionCount = transactions.Count
        };
    }
}
