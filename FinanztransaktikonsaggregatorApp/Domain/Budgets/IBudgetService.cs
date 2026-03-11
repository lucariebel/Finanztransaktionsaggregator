using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Budgets;

public interface IBudgetService
{
	List<Budget> GetAllBudgets();
	decimal GetUsedBudget(string categorie);
	decimal CalculateRest(decimal budget, string categorie);

	decimal CalculatePercentage(decimal budget, string categorie);
	Budget AddNewBudget(string categorie, decimal limit);
}