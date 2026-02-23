namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ImportDataCommand : ICommand
{
    public string Name { get; } = "Import Data (CSV)";
    
    public void Execute()
    {
        Console.WriteLine("Start Import...");
        Console.ReadKey();
    }
}