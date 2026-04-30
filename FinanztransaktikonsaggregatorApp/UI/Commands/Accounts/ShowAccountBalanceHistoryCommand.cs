using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;
using FinanztransaktikonsaggregatorApp.Domain.Accounts.History;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;

public class ShowAccountBalanceHistoryCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly IAccountBalanceHistoryService _balanceHistoryService;

    public ShowAccountBalanceHistoryCommand(
        IAccountService accountService,
        IAccountBalanceHistoryService balanceHistoryService)
    {
        _accountService = accountService;
        _balanceHistoryService = balanceHistoryService;
    }

    public string Name { get; } = "Show Balance History";

    public void Execute()
    {
        MenuHelper.CreateHeader("ACCOUNT BALANCE HISTORY");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("Do you want to show an account balance history?",
            () => ShowHistory());
    }

    private void ShowHistory()
    {
        var accounts = _accountService.GetAll();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts have been defined.");
            WaitForReturn();
            return;
        }

        PrintAccounts(accounts);

        Console.WriteLine();
        Console.WriteLine("Please enter the account ID.");
        Console.WriteLine("Press Enter to cancel.");

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            return;

        var accountId = InputHelper.GetValidId(
            "Invalid ID! Please enter a valid account ID:",
            id => _accountService.GetById(id) is not null,
            input
        );

        var startDate = InputHelper.GetDateTime("Start date (yyyy-mm-dd, Enter for no start date): ");
        var endDate = InputHelper.GetDateTime("End date (yyyy-mm-dd, Enter for no end date): ");

        try
        {
            var history = _balanceHistoryService.GetHistory(accountId, startDate, endDate);
            PrintHistory(history);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
        }

        WaitForReturn();
    }

    private static void PrintAccounts(List<Account> accounts)
    {
        const int cols = 4;

        Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "ID", "Name", "Institution", "Initial Balance");
        MenuHelper.CreateHorizontalLine();

        foreach (var account in accounts)
        {
            Console.WriteLine(
                MenuHelper.TableFormatterBudget(cols),
                account.Id,
                account.Name,
                account.Institution,
                $"{account.InitialBalance:C2}");
        }
    }

    private static void PrintHistory(AccountBalanceHistory history)
    {
        Console.WriteLine();
        Console.WriteLine($"Account: {history.Account.Name} ({history.Account.Institution})");
        Console.Write("Opening Balance: ");
        ConsoleHelper.WriteColoredAmount(history.OpeningBalance, 0);
        Console.WriteLine();
        Console.WriteLine();

        if (history.Transactions.Count == 0)
        {
            Console.WriteLine("No transactions found for the selected period.");
        }
        else
        {
            const int cols = 5;
            var colWidth = GetTableColumnWidth(cols);

            Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "Date", "Amount", "Balance", "Category", "Description");
            MenuHelper.CreateHorizontalLine();

            var runningBalance = history.OpeningBalance;

            foreach (var transaction in history.Transactions)
            {
                runningBalance += transaction.Amount;

                WriteTableCell($"{transaction.Date:yyyy-MM-dd}", colWidth, true);
                Console.Write(" | ");
                ConsoleHelper.WriteColoredAmount(transaction.Amount, colWidth);
                Console.Write(" | ");
                ConsoleHelper.WriteColoredAmount(runningBalance, colWidth);
                Console.Write(" | ");
                WriteTableCell(transaction.Category, colWidth);
                Console.Write(" | ");
                WriteTableCell(transaction.Description ?? string.Empty, colWidth);
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.Write("Closing Balance: ");
        ConsoleHelper.WriteColoredAmount(history.ClosingBalance, 0);
        Console.WriteLine();
    }

    private static void WaitForReturn()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    private static void WriteTableCell(object value, int padding, bool alignLeft = false)
    {
        var text = value.ToString() ?? string.Empty;

        if (text.Length > padding)
            text = text[..padding];

        Console.Write(alignLeft ? text.PadRight(padding) : text.PadLeft(padding));
    }

    private static int GetTableColumnWidth(int cols)
    {
        var separatorWidth = (cols - 1) * 3;
        return (Console.WindowWidth - separatorWidth) / cols;
    }
}
