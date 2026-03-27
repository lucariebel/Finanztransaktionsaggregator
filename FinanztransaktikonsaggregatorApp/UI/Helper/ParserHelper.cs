namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class ParserHelper
{
    public static int ParseRequiredId(string input)
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
        while (!decimal.TryParse(input, out number))
        {
            Console.WriteLine("Invalid number. Please enter a valid number:");
            input = Console.ReadLine();
        }
        return number;
    }

    public static decimal? ParseOptinalDecimal(string input)
    {
        decimal number;
        while (true)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            else if (!decimal.TryParse(input, out number))
            {
                Console.WriteLine("Invalid number. Please enter a valid number (or Enter to skip):");
                input = Console.ReadLine();
                continue;
            }
            return number;
        }
    }

    public static int? ParseOptionalInt(string input, int lowerBoundary, int? upperBoundary, string message)
    {
        int number;
        while (true)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            else if(upperBoundary is null)
            {
                if (!int.TryParse(input, out number) || number < lowerBoundary)
                {
                    Console.WriteLine(message);
                    input = Console.ReadLine();
                    continue;
                }
            }
            else
            {
                if (!int.TryParse(input, out number) || number < lowerBoundary || number > upperBoundary)
                {
                    Console.WriteLine(message);
                    input = Console.ReadLine();
                    continue;
                }
            }
            return number;
        }
        
    }
}