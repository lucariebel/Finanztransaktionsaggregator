using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Budgets;

public interface IBudgetService
{
	List<Budget> getAllBudgets();
	decimal getUsedBudget(string categorie);
	decimal calculateRest(decimal budget, string categorie);

	decimal calculatePercentage(decimal budget, string categorie);
	Budget addNewBudget(string categorie, decimal limit);
}