using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class AddAccountCommand : ICommand
{
    private readonly IAccountService _accountService;

    public AddAccountCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name { get; } = "Add Account";

    public void Execute()
    {
        MenuHelper.CreateHeader("ADD ACCOUNT");
        Console.WriteLine();

        var name = InputHelper.GetRequiredString("Please enter the account name:");
        var institution = InputHelper.GetRequiredString("Please enter the institution:");
        var initialBalance = InputHelper.GetRequiredDecimal("Please enter the initial balance:");

        var account = new Account
        {
            Name = name,
            Institution = institution,
            InitialBalance = initialBalance
        };

        var createdAccount = _accountService.Insert(account);

        Console.WriteLine();
        Console.WriteLine($"Account successfully added: ID {createdAccount.Id}, Name {createdAccount.Name}, Institution {createdAccount.Institution}, Initial Balance {createdAccount.InitialBalance:C2}");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}