namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class MenuHelper
{
    public static void CreateHeader(string title)
    {
        var windowWidth = Console.WindowWidth;

        Console.WriteLine(new string('=', windowWidth));

        var padding = (windowWidth + title.Length) / 2;
        Console.WriteLine(title.PadLeft(padding).PadRight(windowWidth));

        Console.WriteLine(new string('=', windowWidth));
    }

    public static void CreateHorizontalLine()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));
    }
}