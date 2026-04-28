using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class ShowAccountsCommand : ICommand
{
    private readonly IAccountService _accountService;

    public ShowAccountsCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name { get; } = "Show Accounts";

    public void Execute()
    {
        MenuHelper.CreateHeader("ACCOUNTS OVERVIEW");
        Console.WriteLine();

        var accounts = _accountService.GetAll();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts have been defined.");
        }
        else
        {
            Console.WriteLine("{0,-8} | {1,-25} | {2,-25} | {3,15}", "ID", "Name", "Institution", "Initial Balance");
            MenuHelper.CreateHorizontalLine();

            foreach (var account in accounts)
            {
                Console.WriteLine(
                    "{0,-8} | {1,-25} | {2,-25} | {3,15:C2}",
                    account.Id,
                    account.Name,
                    account.Institution,
                    account.InitialBalance
                );
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}