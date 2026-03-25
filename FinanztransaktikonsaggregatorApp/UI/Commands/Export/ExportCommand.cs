using FinanztransaktikonsaggregatorApp.Controllers.Helper;

namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ExportCommand : ICommand
{
    public string Name { get; } = "Export";

    public void Execute()
    {
        MenuHelper.CreateHeader("IMPORT DATA");
        Console.WriteLine();
        Console.ReadKey();
    }
}