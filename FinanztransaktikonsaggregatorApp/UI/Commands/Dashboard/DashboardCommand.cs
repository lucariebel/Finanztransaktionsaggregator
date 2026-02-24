using FinanztransaktikonsaggregatorApp.Controllers.Helper;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

public class DashboardCommand : ICommand
{
    public string Name { get; } = "Dashboard";
    public void Execute()
    {
        MenuHelper.CreateHeader("FINANCIAL DASHBOARD");
        Console.WriteLine();
        Console.WriteLine($"Month: {DateTime.Now:MMMM}");
        Console.WriteLine("Total Transactions: 10");
        Console.WriteLine();
        Console.WriteLine("[ BALANCE OVERVIEW ]");
        Console.WriteLine($"Current Balance: {100,15:C2}");
        Console.WriteLine($"Total Income:    + {1000,15:C2}");
        Console.WriteLine($"Total Expenses:  - {900,15:C2}");
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine($"Net Savings:      {(1000-900),15:C2}");
        Console.WriteLine();
        Console.WriteLine("[ TOP 3 EXPENSES ]");
        Console.WriteLine();
        Console.WriteLine("Press key to return");
        Console.ReadKey();
    }
}