
using QuestPDF.Fluent;

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
        int id = ParserHelper.ParseRequiredId(input);

        while (!validator(id))
        {
            Console.WriteLine(message);
            input = Console.ReadLine();
            id = ParserHelper.ParseRequiredId(input);
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

    public static string GetExistingFilePath(string message, string filePath)
    {
        while (!File.Exists(filePath))
        {
            Console.WriteLine(message);
            filePath = Console.ReadLine();
        }
        return filePath;
    }

    public static List<int> GetIntList(string message)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine($"{message}");
        string input = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(input))
        {
            string[] parts = input.Split(',');
            foreach (string part in parts)
            {
                if (int.TryParse(part.Trim(), out int result))
                {
                    numbers.Add(result);
                }
                else
                {
                    Console.WriteLine($"'{part}' ist keine gültige Zahl und wird übersprungen.");
                }
            }
        }
        return numbers;
    }
    public static List<string> GetStringList(string message)
    {
        List<string> strings = new List<string>();
        Console.WriteLine($"{message}");
        string input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            string[] parts = input.Split(',');
            foreach (string part in parts)
            {
                string trimmed = part.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    strings.Add(trimmed);
                }
            }
        }
        return strings;
    }

    public static DateTime? GetDateTime(string message)
    {
        while (true)
        {
            Console.Write(message+"");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (DateTime.TryParse(input, out DateTime result))
                return result;

            Console.WriteLine("Unvalid date. Please Enter in Format yyyy-mm-dd or enter for no date.");
        }
    }
    public static int AskForAccountNumber(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();

            try
            {
                return ParserHelper.ParseRequiredId(input);
            }
            catch
            {
                Console.WriteLine("Ungültige Account Number. Bitte erneut versuchen.");
            }
        }
    }
}
