using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Budgets;

public interface IBudgetService
{
	List<Budget> GetAllBudgets();
	decimal GetUsedBudget(string category, int year, int month);
	decimal GetIncome(string category, int year, int month);
	decimal CalculateRest(decimal budget, string category, int year, int month);

	decimal CalculatePercentage(decimal budget, string category, int year, int month);
	Budget AddNewBudget(string category, decimal limit);
	void DeleteBudget(int id);
	Budget UpdateBudget(int id, string category, decimal limit);

	bool IsValidId(int id);
}