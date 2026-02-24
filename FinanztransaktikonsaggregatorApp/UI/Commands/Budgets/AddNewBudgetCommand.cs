namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class AddNewBudgetCommand : ICommand
{
    public string Name { get; } = "Add new budget";
    public void Execute()
    {
        Console.ReadKey();
    }
}