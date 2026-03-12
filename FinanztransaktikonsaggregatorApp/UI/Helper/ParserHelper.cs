namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class ParserHelper
{
    public static int ParseInteger(string input)
    {
        int number;
        while (!int.TryParse(input, out number) || number <= 0)
        {
            Console.WriteLine("Invalid number. Please enter a valid positive id:");
            input = Console.ReadLine();
        }
        return number;
    }

    public static decimal ParseDecimal(string input)
    {
        decimal number;
        while (!decimal.TryParse(input, out number) || number <= 0)
        {
            Console.WriteLine("Invalid number. Please enter a valid positive id:");
            input = Console.ReadLine();
        }
        return number;
    }
}