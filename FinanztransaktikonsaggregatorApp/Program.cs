using FinanztransaktikonsaggregatorApp.Controllers;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;
using FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;
using FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

var mainCommands = new List<ICommand>
{
    new DashboardCommand(),
    new BudgetsCommand(),
    new ImportDataCommand(),
    new ExportCommand()
};

var mainMenu = new MenuController("Home", mainCommands);
mainMenu.Run();