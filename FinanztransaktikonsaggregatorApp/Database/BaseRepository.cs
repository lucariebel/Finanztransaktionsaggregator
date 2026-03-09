using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorApp.Database;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    protected BaseRepository(AppConfig config)
    {
        _connectionString = config.DatabaseConnectionString;
    }

    protected SqliteConnection GetOpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}