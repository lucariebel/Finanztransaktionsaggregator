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

    private string TableFormatter()
    {
        int width = Console.WindowWidth;

        int cols = 5;

        int separatorWidth = (cols - 1) * 3;

        int colWidth = (width - separatorWidth) / cols;

        string format =
            $"{{0,-{colWidth}}} | " +
            $"{{1,{colWidth}}} | " +
            $"{{2,{colWidth}}} | " +
            $"{{3,{colWidth}}} | " +
            $"{{4,{colWidth}}}";

        return format;
    }

    public void Execute()
    {
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
            Console.WriteLine(TableFormatter(), "Category", "Limit", "Used", "Leftover", "%");
            MenuHelper.CreateHorizontalLine();
            foreach (var budget in budgets)
            {
                Console.WriteLine(TableFormatter(), budget.Category, $"{budget.LimitAmount:C2}", $"{_budgetService.GetUsedBudget(budget.Category):C2}", $"{_budgetService.CalculateRest(budget.LimitAmount, budget.Category):C2}", $"{_budgetService.CalculatePercentage(budget.LimitAmount, budget.Category)} %");
            }

            Console.ReadKey();
        }

    }
}