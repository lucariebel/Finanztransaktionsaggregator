using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorApp.Database;

public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(AppConfig config)
    {
        _connectionString = config.DatabaseConnectionString;
    }

    public void Initialize()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var createTransactionsTable = @"
                    CREATE TABLE IF NOT EXISTS Transactions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT NOT NULL,
                        Amount REAL NOT NULL,
                        Description TEXT,
                        Category TEXT,
                        AccountNumber TEXT
                    );";

            var createBudgetsTable = @"
                    CREATE TABLE IF NOT EXISTS Budgets (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Category TEXT UNIQUE NOT NULL,
                        LimitAmount REAL NOT NULL
                    );";

            var createStocksTable = @"
                    CREATE TABLE IF NOT EXISTS Stocks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TickerSymbol TEXT,
                        Name TEXT,
                        Quantity REAL NOT NULL,
                        AverageBuyPrice REAL NOT NULL,
                        LastKnownPrice REAL,
                        LastUpdated TEXT
                    );";

            var createAccountsTable = @"
                    CREATE TABLE IF NOT EXISTS Accounts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Institution TEXT,
                        InitialBalance REAL NOT NULL
                    );";

            using (var command = new SqliteCommand(createTransactionsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SqliteCommand(createBudgetsTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SqliteCommand(createStocksTable, connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SqliteCommand(createAccountsTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}