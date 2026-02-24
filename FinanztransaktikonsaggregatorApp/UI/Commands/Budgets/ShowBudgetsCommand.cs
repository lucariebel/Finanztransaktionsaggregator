using FinanztransaktikonsaggregatorApp.Controllers.Helper;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class ShowBudgetsCommand : ICommand
{
    public string Name { get; } = "Show Budgets";
    public void Execute()
    {
        MenuHelper.CreateHeader("BUDGETS OVERVIEW");

        Console.ReadKey();
    }
}