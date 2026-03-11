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

    public decimal GetUsedBudget(string category)
    {
        decimal usedBudget = _transactionService.getTransactionsBycategory(category);

        return usedBudget;

    }

    public decimal CalculateRest(decimal budget, string category)
    {
        decimal used = GetUsedBudget(category);
        decimal rest = budget - used;
        return rest;
    }

    public decimal CalculatePercentage(decimal budget, string category)
    {
        decimal used = GetUsedBudget(category); ;
        decimal percentage = used / budget;
        return percentage * 100;
    }

    public Budget AddNewBudget(string categorys, decimal limit)
    {
        Budget newBudget = new Budget
        {
            Category = categorys,
            LimitAmount = limit
        };
        return _budgetRepository.Insert(newBudget);
        
    }
}