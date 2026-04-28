using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Domain.Category;
using FinanztransaktikonsaggregatorApp.Domain.Accounts;
using FinanztransaktikonsaggregatorApp.Domain.Dashboard;
using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Domain.Stocks;
using FinanztransaktikonsaggregatorApp.UI;
using FinanztransaktikonsaggregatorApp.UI.Commands;
using FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;
using FinanztransaktikonsaggregatorApp.UI.Commands.Dashboard;
using FinanztransaktikonsaggregatorApp.UI.Commands.Import;
using FinanztransaktikonsaggregatorApp.UI.Commands.Accounts;
using FinanztransaktikonsaggregatorApp.UI.Commands.Stocks;

var appConfig = new AppConfig();
var dbManager = new DatabaseManager(appConfig);
var transactionRepo = new TransactionRepository(appConfig);
var transactionService = new TransactionService(transactionRepo);
var accountRepo = new AccountRepository(appConfig);
var accountService = new AccountService(accountRepo);
var budgetRepository = new BudgetRepository(appConfig);
var budgetService = new BudgetService(budgetRepository, transactionService);
var dashboardService = new DashboardService(transactionService, accountService);
var categoryService = new CategoryService();
var importService = new ImportsService(transactionService, categoryService);
var exportService = new ExportService(transactionService);
var stockRepository = new StockRepository(appConfig);
var stockService = new StockService(stockRepository);


dbManager.Initialize();
Console.OutputEncoding = System.Text.Encoding.UTF8;

var mainCommands = new List<ICommand>
{
    new DashboardCommand(dashboardService, budgetService, transactionService),
    new BudgetMenuCommand(budgetService),
    new ImportDataCommand(importService),
    new ExportCommand(exportService),
    new AccountMenuCommand(accountService),
    new StockMenuCommand(stockService)

};

var mainMenu = new MenuController("Home", mainCommands);
mainMenu.Run();