using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class ShowBudgetsCommand : ICommand
{
    private readonly IBudgetService _budgetService;

    public ShowBudgetsCommand(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    public string Name { get; } = "Show Budgets";

    private string TimeSpan()
    {
        DateTime now = DateTime.Now;

        DateTime start = new DateTime(now.Year, now.Month, 1);
        DateTime end = start.AddMonths(1).AddDays(-1);

        string timespan  = $"{start:dd.MM} - {end:dd.MM}";
        return timespan;

    }

    public void Execute()
    {
        int cols = 5;
        var budgets = _budgetService.GetAllBudgets();
        
        MenuHelper.CreateHeader("BUDGETS OVERVIEW");
        Console.WriteLine();
        if (budgets.Count == 0)
        {
            Console.WriteLine("No budgets have been defined.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"Timespan: {TimeSpan()}");
            Console.WriteLine();
            Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "Category", "Limit", "Used", "Leftover", "%");
            MenuHelper.CreateHorizontalLine();
            foreach (var budget in budgets)
            {
                Console.WriteLine(MenuHelper.TableFormatterBudget(cols), budget.Category, $"{budget.LimitAmount:C2}", $"{_budgetService.GetUsedBudget(budget.Category):C2}", $"{_budgetService.CalculateRest(budget.LimitAmount, budget.Category):C2}", $"{_budgetService.CalculatePercentage(budget.LimitAmount, budget.Category)} %");
            }

            Console.ReadKey();
        }

    }
}