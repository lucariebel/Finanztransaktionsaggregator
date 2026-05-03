
using QuestPDF.Fluent;
using System.Transactions;
using FinanztransaktikonsaggregatorApp.Filter;

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

    public static TransactionsFilter ReadFilter()
    {
        var filter = new TransactionsFilter();

        filter.AccountNumbers = InputHelper.GetIntList(
            "Enter account numbers separated by comma, or press Enter for all accounts:");

        filter.Categories = InputHelper.GetStringList(
            "Enter categories separated by comma, or press Enter for all categories:");

        filter.StartDate = InputHelper.GetDateTime(
            "Enter start date, or press Enter to skip (format: yyyy-mm-dd): ");

        filter.EndDate = InputHelper.GetDateTime(
            "Enter end date, or press Enter to skip (format: yyyy-mm-dd): ");

        if (!filter.StartDate.HasValue && !filter.EndDate.HasValue)
        {
            Console.WriteLine("Enter year, or press Enter to skip:");
            filter.Year = ParserHelper.ParseOptionalInt(
                Console.ReadLine(),
                1,
                null,
                "Invalid year. Please enter a positive number, or press Enter to skip:");

            Console.WriteLine("Enter month, or press Enter to skip:");
            filter.Month = ParserHelper.ParseOptionalInt(
                Console.ReadLine(),
                1,
                12,
                "Invalid month. Please enter a number between 1 and 12, or press Enter to skip:");

            Console.WriteLine("Enter day, or press Enter to skip:");
            filter.Day = ParserHelper.ParseOptionalInt(
                Console.ReadLine(),
                1,
                31,
                "Invalid day. Please enter a number between 1 and 31, or press Enter to skip:");
        }

        Console.WriteLine("Enter minimum amount, or press Enter to skip:");
        filter.MinAmount = ParserHelper.ParseOptinalDecimal(Console.ReadLine());

        Console.WriteLine("Enter maximum amount, or press Enter to skip:");
        filter.MaxAmount = ParserHelper.ParseOptinalDecimal(Console.ReadLine());

        Console.WriteLine("Enter description keyword, or press Enter to skip:");
        filter.DescriptionContains = Console.ReadLine();

        return filter;
    }
}
