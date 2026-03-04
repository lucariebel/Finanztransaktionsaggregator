namespace FinanztransaktikonsaggregatorApp;

public class AppConfig
{
    public string DatabaseConnectionString { get; }

    public AppConfig()
    {
        string dbPath = "finanz_aggregator.db";
            
        DatabaseConnectionString = $"Data Source={dbPath}";
    }
}