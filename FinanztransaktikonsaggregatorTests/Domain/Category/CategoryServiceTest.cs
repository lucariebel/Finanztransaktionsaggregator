using FinanztransaktikonsaggregatorApp.Domain.Category;

namespace FinanztransaktikonsaggregatorTests.Domain.Category;

public class CategoryServiceTests
{
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _service = new CategoryService();
    }

    [Fact]
    public void GetCategoryForDescription_ReturnsLebensmittel()
    {
        // ARRANGE
        var description = "Einkauf bei Aldi";

        // ACT
        var result = _service.GetCategoryForDescription(description);

        // ASSERT
        Assert.Equal("Lebensmittel", result);
    }

    [Fact]
    public void GetCategoryForDescription_ReturnsRestaurant()
    {
        // ARRANGE
        var description = "Pizza Restaurant Bestellung";

        // ACT
        var result = _service.GetCategoryForDescription(description);

        // ASSERT
        Assert.Equal("Restaurant", result);
    }

    [Fact]
    public void GetCategoryForDescription_ReturnsTransport()
    {
        // ARRANGE
        var description = "DB Ticket Berlin";

        // ACT
        var result = _service.GetCategoryForDescription(description);

        // ASSERT
        Assert.Equal("Transport", result);
    }

    [Fact]
    public void GetCategoryForDescription_ReturnsUncategorized_WhenNoMatch()
    {
        // ARRANGE
        var description = "Irgendein unbekannter Text";

        // ACT
        var result = _service.GetCategoryForDescription(description);

        // ASSERT
        Assert.Equal("Uncategorized", result);
    }

    [Fact]
    public void GetCategoryForDescription_ReturnsUncategorized_WhenEmpty()
    {
        // ACT
        var result = _service.GetCategoryForDescription("");

        // ASSERT
        Assert.Equal("Uncategorized", result);
    }
}