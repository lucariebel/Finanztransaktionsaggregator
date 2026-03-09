using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IAccountRepository _accountRepo;
    private readonly ITransactionRepository _transactionRepo;

    public DashboardService(ITransactionRepository transactionRepo, IAccountRepository accountRepo)
    {
        _transactionRepo = transactionRepo;
        _accountRepo = accountRepo;
    }

    public decimal GetTotalNetWorth()
    {
        var totalInitialBalances = _accountRepo.GetAll().Sum(a => a.InitialBalance);

        var totalTransactionSum = _transactionRepo.GetAll().Sum(t => t.Amount);

        return totalInitialBalances + totalTransactionSum;
    }

    public Dictionary<Account, decimal> GetBalancesPerAccount()
    {
        var allTransactions = _transactionRepo.GetAll();
        var allAccounts = _accountRepo.GetAll();

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
}