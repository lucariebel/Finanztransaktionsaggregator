namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class DeleteBudgetCommand : ICommand
{
    public string Name { get; } = "Delete budget";

    public void Execute()
    {
        Console.ReadKey();
    }
}