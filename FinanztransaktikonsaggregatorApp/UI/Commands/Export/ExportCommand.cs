namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ExportCommand : ICommand
{
    public string Name { get; } = "Export";
    public void Execute()
    {
        Console.WriteLine("...");
        Console.ReadKey();
    }
}