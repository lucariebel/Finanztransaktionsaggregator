namespace FinanztransaktikonsaggregatorApp;

public class AppConfig
{
    public AppConfig()
    {
        var dbPath = "finanz_aggregator.db";

        DatabaseConnectionString = $"Data Source={dbPath}";
    }

    public AppConfig(string dbPath)
    {
        DatabaseConnectionString = $"Data Source={dbPath}";
    }

    public string DatabaseConnectionString { get; }
}