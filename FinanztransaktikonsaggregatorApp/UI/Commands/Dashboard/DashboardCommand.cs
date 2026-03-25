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
        Console.WriteLine($"Net Worth: {_dashboardService.GetTotalNetWorth(),15:C2}");
    }

    private void PrintAccountOverview()
    {
        Console.WriteLine("[ ACCOUNT OVERVIEW ]");
        foreach (var balance in _dashboardService.GetBalancesPerAccount())
        {
            var displayName = $"{balance.Key.Name}:";
            Console.WriteLine($"{displayName,-15} {balance.Value,10:C2}");
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
                Console.WriteLine($"{expense.Date:yyyy-MM-dd}  {expense.Amount,12:C2}  {expense.Description}");
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
                Console.WriteLine($"{budget.Category,-15} ({_budgetService.CalculatePercentage(budget.LimitAmount , budget.Category)}%) {budget.LimitAmount,10:C2}  WARNING  {_budgetService.CalculateRest(budget.LimitAmount , budget.Category),10:C2} left");
            }
        }
    }
}