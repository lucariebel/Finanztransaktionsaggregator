using FinanztransaktikonsaggregatorApp.Domain.Accounts;
using FinanztransaktikonsaggregatorApp.Domain.Accounts.History;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class AccountMenuCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly IAccountBalanceHistoryService _balanceHistoryService;

    public AccountMenuCommand(IAccountService accountService, IAccountBalanceHistoryService balanceHistoryService)
    {
        _accountService = accountService;
        _balanceHistoryService = balanceHistoryService;
    }

    public string Name { get; } = "Accounts";

    public void Execute()
    {
        var menuCommands = new List<ICommand>
        {
            new ShowAccountsCommand(_accountService),
            new ShowAccountBalanceHistoryCommand(_accountService, _balanceHistoryService),
            new AddAccountCommand(_accountService),
            new UpdateAccountCommand(_accountService),
            new DeleteAccountCommand(_accountService)
        };

        var accountMenu = new MenuController("ACCOUNT MANAGEMENT", menuCommands);
        accountMenu.Run();
    }
}