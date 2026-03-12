using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class MenuHelper
{
    public static void CreateHeader(string title)
    {
        var windowWidth = Console.WindowWidth;

        Console.WriteLine(new string('=', windowWidth));

        var padding = (windowWidth + title.Length) / 2;
        Console.WriteLine(title.PadLeft(padding).PadRight(windowWidth));

        Console.WriteLine(new string('=', windowWidth));
    }

    public static void CreateHorizontalLine()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));
    }

    public static string TableFormatterBudget(int cols)
    {
        int width = Console.WindowWidth;

        int separatorWidth = (cols - 1) * 3;

        int colWidth = (width - separatorWidth) / cols;

        var parts = new List<string>();

        for (int i = 0; i < cols; i++)
        {
            if (i == 0) 
                parts.Add($"{{{i},-{colWidth}}}");
            else
                parts.Add($"{{{i},{colWidth}}}");
        }

        return string.Join(" | ", parts);
    }

    public static void BudgetList(int cols, List<Budget> budgets)
    {
        Console.WriteLine("Your Budgets:");
        MenuHelper.CreateHorizontalLine();

        Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "Category", "ID", "Limit");
        if (budgets.Count == 0)
        {
            Console.WriteLine("No budgets have been defined.");
        }
        else
        {
            foreach (var budget in budgets)
            {
                Console.WriteLine(MenuHelper.TableFormatterBudget(cols), $"{budget.Category}", budget.Id, $"{budget.LimitAmount:C2}");
            }
        }

    }
}