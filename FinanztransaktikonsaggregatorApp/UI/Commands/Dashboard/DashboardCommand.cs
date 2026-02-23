using FinanztransaktikonsaggregatorApp.Controllers.Helper;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

public class DashboardCommand : ICommand
{
    public string Name { get; } = "Dashboard";
    public void Execute()
    {
        MenuHelper.CreateHeader("FINANCIAL DASHBOARD");
        Console.WriteLine();
        Console.WriteLine("Total Transactions: 10");
        Console.WriteLine();
        Console.WriteLine("[ BALANCE OVERVIEW ]");
        Console.WriteLine("Current Balance: 100€");
        Console.WriteLine("Total Income:    + 1000€");
        Console.WriteLine("Total Expenses:  - 900");
        Console.WriteLine();
        Console.WriteLine("Press key to return");
        Console.ReadKey();
    }
}