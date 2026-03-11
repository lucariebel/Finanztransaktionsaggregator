using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Domain.Dashboard;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;
using FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;
using FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;

var appConfig = new AppConfig();
var dbManager = new DatabaseManager(appConfig);
var transactionRepo = new TransactionRepository(appConfig);
var transactionService = new TransactionService(transactionRepo);
var accountRepo = new AccountRepository(appConfig);
var dashboardService = new DashboardService(transactionRepo, accountRepo);
var budgetRepository = new BudgetRepository(appConfig);
var budgetService = new BudgetService(budgetRepository, transactionService);


dbManager.Initialize();
Console.OutputEncoding = System.Text.Encoding.UTF8;

var mainCommands = new List<ICommand>
{
    new DashboardCommand(dashboardService),
    new BudgetMenuCommand(budgetService),
    new ImportDataCommand(),
    new ExportCommand()
};

var mainMenu = new MenuController("Home", mainCommands);
mainMenu.Run();