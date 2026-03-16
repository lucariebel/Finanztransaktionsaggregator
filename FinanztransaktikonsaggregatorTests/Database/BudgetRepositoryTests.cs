using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;
using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorTests.Database;

public class BudgetRepositoryTests : IDisposable
{
    private readonly DatabaseManager _dbManager;
    private readonly BudgetRepository _repo;
    private readonly AppConfig _testConfig;
    private readonly string _testDbPath;

    public BudgetRepositoryTests()
    {
        _testDbPath = $"test_budgets_{Guid.NewGuid()}.db";

        _testConfig = new AppConfig(_testDbPath);

        _dbManager = new DatabaseManager(_testConfig);
        _dbManager.Initialize();

        _repo = new BudgetRepository(_testConfig);
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
        var newBudget = new Budget
        {
            Category = "Lebensmittel",
            LimitAmount = 300.00m
        };

        // ACT
        _repo.Insert(newBudget);
        var results = _repo.GetAll();

        // ASSERT
        Assert.Single(results);

        var savedBudget = results.First();

        Assert.True(savedBudget.Id > 0);
        Assert.Equal("Lebensmittel", savedBudget.Category);
        Assert.Equal(300.00m, savedBudget.LimitAmount);
    }

    [Fact]
    public void Insert_And_UpdateBudget()
    {
        // ARRANGE
        var newBudget = new Budget
        {
            Category = "Lebensmittel",
            LimitAmount = 300.00m
        };

        // ACT
        _repo.Insert(newBudget);
        newBudget.LimitAmount = 450.50m;
        _repo.Update(newBudget);

        var results = _repo.GetAll();

        // ASSERT
        Assert.Single(results);

        var savedBudget = results.First();

        Assert.Equal(1, savedBudget.Id);
        Assert.Equal("Lebensmittel", savedBudget.Category);
        Assert.Equal(450.50m, savedBudget.LimitAmount);
    }

    [Fact]
    public void Insert_And_DeleteBudget()
    {
        // ARRANGE
        var newBudget = new Budget
        {
            Category = "Lebensmittel",
            LimitAmount = 300.00m
        };

        // ACT
        _repo.Insert(newBudget);
        _repo.Delete(newBudget.Id);

        var results = _repo.GetAll();

        // ASSERT
        Assert.Empty(results);
    }
}