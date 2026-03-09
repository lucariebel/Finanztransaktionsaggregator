using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Dashboard;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;
using FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;
using FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

var appConfig = new AppConfig();
var dbManager = new DatabaseManager(appConfig);
var transactionRepo = new TransactionRepository(appConfig);
var accountRepo = new AccountRepository(appConfig);
var dashboardService = new DashboardService(transactionRepo, accountRepo);

dbManager.Initialize();

var mainCommands = new List<ICommand>
{
    new DashboardCommand(dashboardService),
    new BudgetMenuCommand(),
    new ImportDataCommand(),
    new ExportCommand()
};

var mainMenu = new MenuController("Home", mainCommands);
mainMenu.Run();