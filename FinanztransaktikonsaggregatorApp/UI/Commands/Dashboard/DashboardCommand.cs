using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Domain.Dashboard;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

public class DashboardCommand : ICommand
{
    private readonly IDashboardService _dashboardService;
    private readonly IBudgetService _budgetService;
    private readonly ITransactionService _transactionService;

    public DashboardCommand(IDashboardService dashboardService, IBudgetService budgetService, ITransactionService transactionService)
    {
        _dashboardService = dashboardService;
        _budgetService = budgetService;
        _transactionService = transactionService;
    }

    public string Name { get; } = "Dashboard";

    public void Execute()
    {
        MenuHelper.CreateHeader("FINANCIAL DASHBOARD");
        PrintNetWorth();
        MenuHelper.CreateHorizontalLine();
        PrintAccountOverview();

        Console.WriteLine();
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine($"Month: {DateTime.Now:MMMM}");

        PrintTopExpenses();
        PrintBudgetWarnings();

        Console.WriteLine();
        Console.WriteLine("Press key to return");
        Console.ReadKey();
    }

    private void PrintNetWorth()
    {
        Console.WriteLine();
        var netWorth = _dashboardService.GetTotalNetWorth();

        Console.Write("Net Worth: ");
        ConsoleHelper.WriteColoredAmount(netWorth, 15);
        Console.WriteLine();
    }

    private void PrintAccountOverview()
    {
        Console.WriteLine("[ ACCOUNT OVERVIEW ]");
        foreach (var balance in _dashboardService.GetBalancesPerAccount())
        {
            var displayName = $"{balance.Key.Name}:";
            Console.Write($"{displayName,-15} ");
            ConsoleHelper.WriteColoredAmount(balance.Value, 10);
            Console.WriteLine();
        }
    }

    private void PrintTopExpenses()
    {
        Console.WriteLine();
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine("[ TOP 3 EXPENSES ]");

        var topExpenses = _transactionService.GetTopExpenses(3);
        if (topExpenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
        }
        else
        {
            foreach (var expense in topExpenses)
            {
                Console.Write($"{expense.Date:yyyy-MM-dd}  ");
                ConsoleHelper.WriteColoredAmount(expense.Amount, 12);
                Console.WriteLine($"  {expense.Description}");
            }
        }
    }

    private void PrintBudgetWarnings()
    {
        Console.WriteLine();
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine("[ BUDGET WARNINGS (>80%) ]");

        var warnings = _budgetService.GetBudgetWarnings(80);
        if (warnings.Count == 0)
        {
            Console.WriteLine("All budgets are within limits.");
        }
        else
        {
            foreach (var budget in warnings)
            {
                var percentage = _budgetService.CalculatePercentage(budget.LimitAmount, budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth());
                var rest = _budgetService.CalculateRest(budget.LimitAmount, budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth());

                Console.ForegroundColor = percentage >= 100 ? ConsoleColor.Red : ConsoleColor.Yellow;
                Console.Write($"{budget.Category,-15} ({percentage}%) {budget.LimitAmount,10:C2}  WARNING  {rest,10:C2} left");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}
