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
    public string Description { get; } =  "Shows all Budgets within a period.";

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

        int cols = 6;

        int separatorWidth = (cols - 1) * 3;

        int colWidth = (width - separatorWidth) / cols;

        string format =
            $"{{0,-{colWidth}}} | " +
            $"{{1,-{colWidth}}} | " +
            $"{{2,-{colWidth}}} | " +
            $"{{3,-{colWidth}}} | " +
            $"{{4,-{colWidth}}} | " +
            $"{{5,-{colWidth}}}";

        return format;
    }

    public void Execute()
    {
        var budgets = _budgetService.getAllBudgets();
        
        MenuHelper.CreateHeader("BUDGETS OVERVIEW");
        Console.WriteLine();
        if (budgets.Count == 0)
        {
            Console.WriteLine("Es sind noch keine Budgets definiert.");
        }
        else
        {
            Console.WriteLine(TableFormatter(), "Kategorie", "Zeitraum", "Limit", "Verbrauch", "Rest", "%");
            MenuHelper.CreateHorizontalLine();
            foreach (var budget in budgets)
            {
                Console.WriteLine(TableFormatter(), budget.Category, TimeSpan(), budget.LimitAmount, _budgetService.getUsedBudget(budget.Category), _budgetService.calculateRest(budget.LimitAmount, budget.Category), _budgetService.calculatePercentage(budget.LimitAmount, budget.Category));
            }

            Console.ReadKey();
        }

    }
}