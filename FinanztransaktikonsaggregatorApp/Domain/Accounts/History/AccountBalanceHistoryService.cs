using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Accounts.History;

public class AccountBalanceHistoryService : IAccountBalanceHistoryService
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;

    public AccountBalanceHistoryService(IAccountService accountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
    }

    public AccountBalanceHistory GetHistory(int accountId, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (startDate.HasValue && endDate.HasValue && startDate.Value.Date > endDate.Value.Date)
            throw new ArgumentException("Start date must be before or equal to end date.");

        var account = _accountService.GetById(accountId);

        if (account is null)
            throw new ArgumentException($"Account with ID {accountId} does not exist.");

        var accountTransactions = _transactionService.GetByAccountId(accountId);

        var openingBalance = account.InitialBalance + accountTransactions
            .Where(t => startDate.HasValue && t.Date.Date < startDate.Value.Date)
            .Sum(t => t.Amount);

        var visibleTransactions = accountTransactions
            .Where(t => !startDate.HasValue || t.Date.Date >= startDate.Value.Date)
            .Where(t => !endDate.HasValue || t.Date.Date <= endDate.Value.Date)
            .ToList();

        var closingBalance = openingBalance + visibleTransactions.Sum(t => t.Amount);

        return new AccountBalanceHistory
        {
            Account = account,
            StartDate = startDate?.Date,
            EndDate = endDate?.Date,
            OpeningBalance = openingBalance,
            ClosingBalance = closingBalance,
            Transactions = visibleTransactions
        };
    }

    public decimal GetBalanceAt(int accountId, DateTime date)
    {
        return GetHistory(accountId, null, date).ClosingBalance;
    }
}
