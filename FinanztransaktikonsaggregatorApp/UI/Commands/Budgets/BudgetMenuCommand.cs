using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class BudgetMenuCommand : ICommand
{
    private readonly IBudgetService _budgetService;

    public BudgetMenuCommand(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    public string Name { get; } = "Budgets";

    public void Execute()
    {
        var menuCommands = new List<ICommand>
        {
            new ShowBudgetsCommand(_budgetService),
            new AddNewBudgetCommand(_budgetService),
            new DeleteBudgetCommand()
        };

        var budgetMenu = new MenuController("BUDGETS MANAGEMENT", menuCommands);
        budgetMenu.Run();
    }
}