using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Controllers;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;
using FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;
using FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

var appConfig = new AppConfig();
var dbManager = new DatabaseManager(appConfig);
dbManager.Initialize();

var mainCommands = new List<ICommand>
{
    new DashboardCommand(),
    new BudgetMenuCommand(),
    new ImportDataCommand(),
    new ExportCommand()
};

var mainMenu = new MenuController("Home", mainCommands);
mainMenu.Run();