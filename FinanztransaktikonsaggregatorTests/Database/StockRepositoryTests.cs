using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;
using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorTests.Database;

public class StockRepositoryTests : IDisposable
{
    private readonly DatabaseManager _dbManager;
    private readonly StockRepository _repo;
    private readonly AppConfig _testConfig;
    private readonly string _testDbPath;

    public StockRepositoryTests()
    {
        _testDbPath = $"test_stocks_{Guid.NewGuid()}.db";

        _testConfig = new AppConfig(_testDbPath);

        _dbManager = new DatabaseManager(_testConfig);
        _dbManager.Initialize();

        _repo = new StockRepository(_testConfig);
    }

    public void Dispose()
    {
        SqliteConnection.ClearAllPools();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (File.Exists(_testDbPath)) File.Delete(_testDbPath);
    }

    [Fact]
    public void Insert_And_GetAll()
    {
        // ARRANGE
        var newStock = new Stock
        {
            TickerSymbol = "AAPL",
            Name = "Apple Inc.",
            Quantity = 10.5m,
            AverageBuyPrice = 150.25m,
            LastKnownPrice = 175.50m,
            LastUpdated = new DateTime(2026, 3, 9, 14, 0, 0)
        };

        // ACT
        _repo.Insert(newStock);
        var results = _repo.GetAll();

        // ASSERT
        Assert.Single(results);

        var savedStock = results.First();

        Assert.True(savedStock.Id > 0);
        Assert.Equal("AAPL", savedStock.TickerSymbol);
        Assert.Equal("Apple Inc.", savedStock.Name);
        Assert.Equal(10.5m, savedStock.Quantity);
        Assert.Equal(150.25m, savedStock.AverageBuyPrice);
        Assert.Equal(175.50m, savedStock.LastKnownPrice);
        Assert.Equal(new DateTime(2026, 3, 9, 14, 0, 0), savedStock.LastUpdated);
    }

    [Fact]
    public void Insert_And_UpdateStock()
    {
        // ARRANGE
        var newStock = new Stock
        {
            TickerSymbol = "AAPL",
            Name = "Apple Inc.",
            Quantity = 10.5m,
            AverageBuyPrice = 150.25m,
            LastKnownPrice = 175.50m,
            LastUpdated = new DateTime(2026, 3, 9, 14, 0, 0)
        };

        // ACT
        _repo.Insert(newStock);
        newStock.Quantity = 12m;
        _repo.Update(newStock);

        var results = _repo.GetAll();

        // ASSERT
        Assert.Single(results);

        var savedStock = results.First();

        Assert.Equal(1, savedStock.Id);
        Assert.Equal("AAPL", savedStock.TickerSymbol);
        Assert.Equal("Apple Inc.", savedStock.Name);
        Assert.Equal(12m, savedStock.Quantity);
        Assert.Equal(150.25m, savedStock.AverageBuyPrice);
        Assert.Equal(175.50m, savedStock.LastKnownPrice);
        Assert.Equal(new DateTime(2026, 3, 9, 14, 0, 0), savedStock.LastUpdated);
    }

    [Fact]
    public void Insert_And_DeleteStock()
    {
        // ARRANGE
        var newStock = new Stock
        {
            TickerSymbol = "AAPL",
            Name = "Apple Inc.",
            Quantity = 10.5m,
            AverageBuyPrice = 150.25m,
            LastKnownPrice = 175.50m,
            LastUpdated = new DateTime(2026, 3, 9, 14, 0, 0)
        };

        // ACT
        _repo.Insert(newStock);
        _repo.Delete(newStock);

        var results = _repo.GetAll();

        // ASSERT
        Assert.Empty(results);
    }
}