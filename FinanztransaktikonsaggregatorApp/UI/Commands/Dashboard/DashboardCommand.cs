using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Dashboard;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

public class DashboardCommand : ICommand
{
    public string Name { get; } = "Dashboard";
    
    private IDashboardService _dashboardService;

    public DashboardCommand(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    
    public void Execute()
    {
        MenuHelper.CreateHeader("FINANCIAL DASHBOARD");
        Console.WriteLine();
        Console.WriteLine($"Net Worth: {_dashboardService.GetTotalNetWorth(),15:C2}");
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine("[ ACCOUNT OVERVIEW ]");
        foreach (var balance in _dashboardService.GetBalancesPerAccount())
        {
            var displayName = $"{balance.Key.Name}:";
            Console.WriteLine($"{displayName,-15} {balance.Value,10:C2}");
        }
        Console.WriteLine();
        MenuHelper.CreateHorizontalLine();
        Console.WriteLine($"Month: {DateTime.Now:MMMM}");
        Console.WriteLine();
        Console.WriteLine("[ TOP 3 EXPENSES ]");
        Console.WriteLine("Press key to return");
        Console.ReadKey();
    }
}