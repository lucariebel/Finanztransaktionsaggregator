using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI;

public class MenuController
{
    private readonly string _title;
    private readonly List<ICommand> _commands;
    private bool _isRunning = true;
    
    public MenuController(string title, List<ICommand> commands)
    {
        _title = title;
        _commands = commands;
    }
    
    public void Run()
    {
        _isRunning = true;

        while (_isRunning)
        {
            Console.Clear();
            MenuHelper.CreateHeader(_title);

            // List commands
            for (int i = 0; i < _commands.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_commands[i].Name}");
            }
                
            MenuHelper.CreateHorizontalLine();
            Console.WriteLine("[0] Return / Exit");
            Console.WriteLine();
            Console.Write("Please choose: ");

            string input = Console.ReadLine();

            // Return to last page; Exit app if in main menu
            if (input == "0")
            {
                _isRunning = false;
                continue;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= _commands.Count)
            {
                var selectedCommand = _commands[choice - 1];
                Console.Clear();
                selectedCommand.Execute();
            }
            else
            {
                Console.WriteLine("Invalid key!");
                Console.ReadKey();
            }
        }
    }
}