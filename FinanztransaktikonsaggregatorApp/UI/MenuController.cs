using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Middleware;
using FinanztransaktikonsaggregatorApp.UI.Commands;

namespace FinanztransaktikonsaggregatorApp.UI;

public class MenuController
{
    private readonly List<ICommand> _commands;
    private readonly string _title;
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
            for (var i = 0; i < _commands.Count; i++) Console.WriteLine($"[{i + 1}] {_commands[i].Name}");

            MenuHelper.CreateHorizontalLine();
            Console.WriteLine("[ENTER] Return / Exit");
            Console.WriteLine();
            Console.Write("Please choose: ");

            var input = Console.ReadLine();

            // Return to last page; Exit app if in main menu
            if (input is "")
            {
                _isRunning = false;
                continue;
            }

            if (int.TryParse(input, out var choice) && choice > 0 && choice <= _commands.Count)
            {
                var selectedCommand = _commands[choice - 1];
                Console.Clear();
                ExecuteCommand(selectedCommand);
            }
            else
            {
                Console.WriteLine("Invalid key!");
                Console.ReadKey();
            }
        }
    }

    private void ExecuteCommand(ICommand command)
    {
        try
        {
            command.Execute();
        }
        catch (Exception ex)
        {
            ExceptionHandler.Handle(ex);
        }
    }
}