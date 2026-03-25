using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorTests.Mocks;

namespace FinanztransaktikonsaggregatorTests.Domain.Budgets;

public class BudgetServiceTests
{
    private readonly FakeBudgetRepository _budgetRepo;
    private readonly FakeTransactionService _transactionService;
    private readonly BudgetService _service;

    public BudgetServiceTests()
    {
        _budgetRepo = new FakeBudgetRepository();
        _transactionService = new FakeTransactionService();

        _service = new BudgetService(_budgetRepo, _transactionService);
    }

    [Fact]
    public void GetAllBudgets_ReturnsBudgets()
    {
        // ARRANGE
        _budgetRepo.Insert(new Budget { Category = "Food", LimitAmount = 300 });
        _budgetRepo.Insert(new Budget { Category = "Transport", LimitAmount = 100 });

        // ACT
        var result = _service.GetAllBudgets();

        // ASSERT
        Assert.Equal(2, result.Count);
        Assert.Equal("Food", result[0].Category);
    }

    [Fact]
    public void GetUsedBudget_ReturnsTransactionSum()
    {
        // ARRANGE
        var date = new DateTime(2025, 3, 1);

        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = -50, Date = date });
        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = -70, Date = date });

        // ACT
        var result = _service.GetUsedBudget("Food", 2025, 3);

        // ASSERT
        Assert.Equal(120m, result);
    }
    [Fact]
    public void GetIncome_ReturnsCorrectIncome()
    {
        // ARRANGE
        var date = new DateTime(2025, 3, 1);

        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = 100, Date = date });
        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = 50, Date = date });
        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = -30, Date = date });

        // ACT
        var result = _service.GetIncome("Food", 2025, 3);

        // ASSERT
        Assert.Equal(150m, result);
    }

    [Fact]
    public void CalculateRest_ReturnsCorrectRemainingBudget()
    {
        // ARRANGE
        var date = new DateTime(2025, 3, 1);

        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = -100, Date = date });
        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = 50, Date = date });

        // ACT
        var result = _service.CalculateRest(300m, "Food", 2025, 3);

        // ASSERT
        Assert.Equal(250m, result);
    }

    [Fact]
    public void CalculatePercentage_ReturnsCorrectPercentage()
    {
        // ARRANGE
        var date = new DateTime(2025, 3, 1);

        _transactionService.AddTransaction(new Transaction { Category = "Food", Amount = -50, Date = date });

        // ACT
        var result = _service.CalculatePercentage(200m, "Food", 2025, 3);

        // ASSERT
        Assert.Equal(25m, result);
    }

    [Fact]
    public void AddNewBudget_AddsBudget()
    {
        // ACT
        var result = _service.AddNewBudget("Food", 300);

        // ASSERT
        Assert.Single(_budgetRepo.Budgets);
        Assert.Equal("Food", result.Category);
        Assert.Equal(300, result.LimitAmount);
    }

    [Fact]
    public void DeleteBudget_RemovesBudget()
    {
        // ARRANGE
        var budget = _budgetRepo.Insert(new Budget { Category = "Food", LimitAmount = 300 });

        // ACT
        _service.DeleteBudget(budget.Id);

        // ASSERT
        Assert.Empty(_budgetRepo.Budgets);
    }

    [Fact]
    public void UpdateBudget_UpdatesBudget()
    {
        // ARRANGE
        var budget = _budgetRepo.Insert(new Budget { Category = "Food", LimitAmount = 300 });

        // ACT
        var result = _service.UpdateBudget(budget.Id, "Food", 500);

        // ASSERT
        Assert.Equal(500, result.LimitAmount);
        Assert.Equal("Food", result.Category);
    }

    [Fact]
    public void IsValidId_ReturnsTrue_WhenBudgetExists()
    {
        // ARRANGE
        var budget = _budgetRepo.Insert(new Budget { Category = "Food", LimitAmount = 200 });

        // ACT
        var result = _service.IsValidId(budget.Id);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public void IsValidId_ReturnsFalse_WhenBudgetDoesNotExist()
    {
        // ACT
        var result = _service.IsValidId(999);

        // ASSERT
        Assert.False(result);
    }
}