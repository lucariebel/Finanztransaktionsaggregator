using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Mocks;

class FakeBudgetRepository : IBudgetRepository
{
    public List<Budget> Budgets { get; } = new();
    private int _idCounter = 1;

    public List<Budget> GetAll()
    {
        return Budgets;
    }

    public Budget GetById(int id)
    {
        return Budgets.FirstOrDefault(b => b.Id == id);
    }

    public Budget Insert(Budget budget)
    {
        budget.Id = _idCounter++;
        Budgets.Add(budget);
        return budget;
    }

    public Budget Update(Budget budget)
    {
        var existing = Budgets.FirstOrDefault(b => b.Id == budget.Id);

        if (existing == null)
            return null;

        existing.Category = budget.Category;
        existing.LimitAmount = budget.LimitAmount;

        return existing;
    }

    public void Delete(int id)
    {
        var budget = Budgets.FirstOrDefault(b => b.Id == id);

        if (budget != null)
        {
            Budgets.Remove(budget);
        }
    }
}