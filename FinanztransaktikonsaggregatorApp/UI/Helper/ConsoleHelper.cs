
namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class ConsoleHelper
{
    public static void ConfirmAndExecute(
    string question,
    Action actionOnYes
    )
    {
        bool continueLoop = true;

        while (continueLoop)
        {
            Console.WriteLine(question + "\nPress Y (Yes) or Enter (No)");
            var key = Console.ReadKey(true).Key;
            Console.WriteLine();

            if (key == ConsoleKey.Y)
            {
                actionOnYes();
                continueLoop = false;
            }
            else if (key == ConsoleKey.Enter)
            {
                continueLoop = false;
            }
            else
            {
                Console.WriteLine("Invalid key!");
                Console.WriteLine();
            }
        }
    }
}