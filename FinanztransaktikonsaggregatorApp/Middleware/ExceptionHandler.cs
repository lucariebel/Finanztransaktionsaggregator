using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorApp.Middleware;

public static class ExceptionHandler
{
    public static void Handle(Exception exception)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        MenuHelper.CreateHeader("ERROR");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine();

        switch (exception)
        {
            case SqliteException:
                Console.WriteLine("A database error occurred. Please try again.");
                break;

            case FileNotFoundException:
                Console.WriteLine("The specified file was not found.");
                break;

            case DirectoryNotFoundException:
                Console.WriteLine("The specified directory was not found.");
                break;

            case FormatException:
                Console.WriteLine("Invalid data format.");
                break;

            case InvalidOperationException:
                Console.WriteLine($"{exception.Message}");
                break;

            case ArgumentException:
                Console.WriteLine($"{exception.Message}");
                break;

            case IOException:
                Console.WriteLine($"{exception.Message}");
                break;

            default:
                Console.WriteLine("An unexpected error occurred. Please try again.");
                break;
        }

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}