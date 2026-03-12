
namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class InputHelper
{
    public static string GetRequiredString(string message)
    {
        string input = "";

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
        }

        return input;
    }

    public static int GetValidId(string message, Func<int, bool> validator,string input)
    {
        int id = ParserHelper.ParseInteger(input);

        while (!validator(id))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
            id = ParserHelper.ParseInteger(input);
        }

        return id;
    }

    public static decimal GetRequiredDecimal(string message)
    {
        string input = "";

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
        }
        return ParserHelper.ParseDecimal(input);
    }
}
