using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorTests.Mocks;

namespace FinanztransaktikonsaggregatorTests.Domain.Import;

public class ImportsServiceTests
{
    private readonly FakeTransactionService _transactionService;
    private readonly FakeCategoryService _categoryService;
    private readonly ImportsService _service;

    public ImportsServiceTests()
    {
        _transactionService = new FakeTransactionService();
        _categoryService = new FakeCategoryService();

        _service = new ImportsService(_transactionService, _categoryService);
    }

    [Fact]
    public void SaveTransactions_SavesTransactions()
    {
        // ARRANGE
        var transactions = new List<Transaction>
        {
            new Transaction { Category = "Food", Amount = 50 },
            new Transaction { Category = "Food", Amount = 20 }
        };

        // ACT
        var result = _service.SaveTransacitons(transactions);

        // ASSERT
        Assert.Equal("Saved succesfully", result);
        Assert.Equal(2, _transactionService.GetAll().Count);
    }

    [Fact]
    public void MergeList_UpdatesCategories()
    {
        // ARRANGE
        var transaction = new Transaction
        {
            Category = "Uncategorized",
            Amount = 10
        };

        var transactions = new List<Transaction> { transaction };

        var categorized = new List<Transaction>
        {
            new Transaction
            {
                Category = "Restaurant",
                Amount = 10
            }
        };

        categorized[0] = transaction;
        categorized[0].Category = "Restaurant";

        // ACT
        var result = _service.MergeList(categorized, transactions);

        // ASSERT
        Assert.Equal("Restaurant", result[0].Category);
    }

    [Fact]
    public void ImportTransactions_ReturnsTransactions()
    {
        // ARRANGE
        var filePath = "test_import.csv";

        File.WriteAllLines(filePath, new[]
        {
            "Date;Amount;Description;CategoryHint;Account",
            "2024-01-01;50;Aldi Einkauf;Aldi;123",
            "2024-01-02;20;Unknown Shop;Unknown Shop;123"
        });

        // ACT
        var result = _service.ImportTransactions(filePath);

        // ASSERT
        Assert.Equal(2, result.AllTransactions.Count);
        Assert.Single(result.UncategorizedTransactions);

        File.Delete(filePath);
    }
}
