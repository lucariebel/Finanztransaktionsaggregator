using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class UpdateAccountCommand : ICommand
{
    private readonly IAccountService _accountService;

    public UpdateAccountCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name { get; } = "Update Account";

    public void Execute()
    {
        MenuHelper.CreateHeader("UPDATE ACCOUNT");
        Console.WriteLine();

        var accounts = _accountService.GetAll();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts have been defined.");
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            return;
        }

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

        Console.WriteLine();
        Console.WriteLine("Please enter the ID you want to update.");
        Console.WriteLine("Press Enter to cancel.");

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return;
        }

        var id = InputHelper.GetValidId(
            "Invalid ID! Please enter a valid account ID:",
            accountId => _accountService.GetById(accountId) is not null,
            input
        );

        var existingAccount = _accountService.GetById(id);

        if (existingAccount is null)
        {
            Console.WriteLine("Account not found.");
            Console.ReadKey();
            return;
        }

        var name = InputHelper.GetRequiredString("Please enter the new account name:");
        var institution = InputHelper.GetRequiredString("Please enter the new institution:");
        var initialBalance = InputHelper.GetRequiredDecimal("Please enter the new initial balance:");

        existingAccount.Name = name;
        existingAccount.Institution = institution;
        existingAccount.InitialBalance = initialBalance;

        var updatedAccount = _accountService.Update(existingAccount);

        Console.WriteLine();
        Console.WriteLine($"Account successfully updated: ID {updatedAccount.Id}, Name {updatedAccount.Name}, Institution {updatedAccount.Institution}, Initial Balance {updatedAccount.InitialBalance:C2}");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}