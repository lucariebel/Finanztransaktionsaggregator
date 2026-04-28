using FinanztransaktikonsaggregatorApp.Domain.Accounts;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class AccountMenuCommand : ICommand
{
    private readonly IAccountService _accountService;

    public AccountMenuCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name { get; } = "Accounts";

    public void Execute()
    {
        var menuCommands = new List<ICommand>
        {
            new ShowAccountsCommand(_accountService),
            new AddAccountCommand(_accountService),
            new UpdateAccountCommand(_accountService),
            new DeleteAccountCommand(_accountService)
        };

        var accountMenu = new MenuController("ACCOUNT MANAGEMENT", menuCommands);
        accountMenu.Run();
    }
}