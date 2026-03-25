using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Budgets;

public interface IBudgetService
{
	List<Budget> GetAllBudgets();
	List<Budget> GetBudgetWarnings(decimal threshold);
	decimal GetUsedBudget(string category);
	decimal GetIncome(string category);
	decimal CalculateRest(decimal budget, string category);
	decimal CalculatePercentage(decimal budget, string category);
	Budget AddNewBudget(string category, decimal limit);
	void DeleteBudget(int id);
	Budget UpdateBudget(int id, string category, decimal limit);
	bool IsValidId(int id);
}