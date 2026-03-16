using FinanztransaktikonsaggregatorApp.Domain.Category;

namespace FinanztransaktikonsaggregatorTests.Mocks;

class FakeCategoryService : ICategoryService
{
    public string GetCategoryForDescription(string description)
    {
        if (description.Contains("Aldi"))
            return "Lebensmittel";

        return "Uncategorized";
    }
}