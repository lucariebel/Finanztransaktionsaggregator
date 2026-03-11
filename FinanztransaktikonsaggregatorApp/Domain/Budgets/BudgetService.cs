using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Transactions;

namespace FinanztransaktikonsaggregatorApp.Domain.Budgets;

public class BudgetService : IBudgetService
{

    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionService _transactionService;

    public BudgetService(IBudgetRepository budgetRepository, ITransactionService transactionService)
    {
        _budgetRepository = budgetRepository; 
        _transactionService = transactionService;
    } 

    public List<Budget> GetAllBudgets()
    {
        return _budgetRepository.GetAll().ToList();
    }

    public decimal GetUsedBudget(string categorie)
    {
        decimal usedBudget = _transactionService.getTransactionsByCategorie(categorie);

        return usedBudget;

    }

    public decimal CalculateRest(decimal budget, string categorie)
    {
        decimal used = GetUsedBudget(categorie);
        decimal rest = budget - used;
        return rest;
    }

    public decimal CalculatePercentage(decimal budget, string categorie)
    {
        decimal used = GetUsedBudget(categorie); ;
        decimal percentage = used / budget;
        return percentage * 100;
    }

    public Budget AddNewBudget(string categories, decimal limit)
    {
        Budget newBudget = new Budget
        {
            Category = categories,
            LimitAmount = limit
        };
        return _budgetRepository.Insert(newBudget);
        
    }
}