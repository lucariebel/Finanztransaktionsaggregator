namespace FinanztransaktikonsaggregatorApp.Controllers.Helper;

public static class MenuHelper
{
    public static void CreateHeader(string title)
    {
        int windowWidth = Console.WindowWidth;
        
        Console.WriteLine(new string('=', windowWidth));
        
        int padding = (windowWidth + title.Length) / 2;
        Console.WriteLine(title.PadLeft(padding).PadRight(windowWidth));
        
        Console.WriteLine(new string('=', windowWidth));
    }

    public static void CreateHorizontalLine()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));
    }
}