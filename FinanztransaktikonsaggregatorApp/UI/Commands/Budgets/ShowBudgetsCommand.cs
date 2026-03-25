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

    public void Execute()
    {
        int cols = 6;
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
            Console.WriteLine($"Timespan: {DateHelper.TimeSpan()}");
            Console.WriteLine();
            Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "Category", "Limit", "Used", "Extra Income", "Leftover", "%");
            MenuHelper.CreateHorizontalLine();
            foreach (var budget in budgets)
            {
                Console.WriteLine(MenuHelper.TableFormatterBudget(cols), 
                    budget.Category, 
                    $"{budget.LimitAmount:C2}", 
                    $"{_budgetService.GetUsedBudget(budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth()):C2}",
                    $"{_budgetService.GetIncome(budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth()):C2}", 
                    $"{_budgetService.CalculateRest(budget.LimitAmount, budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth()):C2}", 
                    $"{_budgetService.CalculatePercentage(budget.LimitAmount, budget.Category, DateHelper.CurrentYear(), DateHelper.CurrentMonth())} %");
            }

            Console.WriteLine();
            Console.WriteLine("Press key to return");
            Console.ReadKey();
        }

    }
}