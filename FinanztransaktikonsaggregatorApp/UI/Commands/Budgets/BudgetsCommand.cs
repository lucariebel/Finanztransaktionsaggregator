namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class BudgetsCommand : ICommand
{
    public string Name { get; } = "Budgets";
    public void Execute()
    {
        Console.WriteLine("...");
        Console.ReadKey();
    }
}