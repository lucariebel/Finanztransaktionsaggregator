namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class BudgetMenuCommand : ICommand
{
    public string Name { get; } = "Budgets";
    public void Execute()
    {        
        var menuCommands = new List<ICommand>
        {
            new ShowBudgetsCommand(),
            new AddNewBudgetCommand(),
            new DeleteBudgetCommand()
        };   
        
        var budgetMenu = new MenuController("BUDGETS MANAGEMENT", menuCommands);
        budgetMenu.Run();
    }
}