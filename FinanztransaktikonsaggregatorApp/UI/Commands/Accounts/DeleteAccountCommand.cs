using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class DeleteAccountCommand : ICommand
{
    private readonly IAccountService _accountService;

    public DeleteAccountCommand(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name { get; } = "Delete Account";

    public void Execute()
    {
        MenuHelper.CreateHeader("DELETE ACCOUNT");
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

        foreach (var acc in accounts)
        {
            Console.WriteLine(
                "{0,-8} | {1,-25} | {2,-25} | {3,15:C2}",
                acc.Id,
                acc.Name,
                acc.Institution,
                acc.InitialBalance
            );
        }

        Console.WriteLine();
        Console.WriteLine("Please enter the ID you want to delete.");
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

        var account = _accountService.GetById(id);

        if (account is null)
        {
            Console.WriteLine("Account not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Do you really want to delete account '{account.Name}'?");
        Console.WriteLine("Press Y to delete or Enter to cancel.");

        var key = Console.ReadKey(true).Key;

        if (key != ConsoleKey.Y)
        {
            return;
        }

        _accountService.Delete(account);

        Console.WriteLine();
        Console.WriteLine("Account successfully deleted.");
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}