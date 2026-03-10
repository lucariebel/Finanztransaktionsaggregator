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

    public List<Budget> getAllBudgets()
    {
        return _budgetRepository.GetAll().ToList();
    }

    public decimal getUsedBudget(string categorie)
    {
        List<Transaction> transactions = _transactionService.getTransactionsByCategorie(categorie);
        decimal usedBudget = transactions.Sum(t => t.Amount);
        return usedBudget;

    }

    public decimal calculateRest(decimal budget, string categorie)
    {
        decimal used = getUsedBudget(categorie);
        decimal rest = budget - used;
        return rest;
    }

    public decimal calculatePercentage(decimal budget, string categorie)
    {
        decimal used = getUsedBudget(categorie); ;
        decimal percentage = used / budget;
        return percentage * 100;
    }

    public Budget addNewBudget(string categories, decimal limit)
    {
        Budget newBudget = new Budget
        {
            Category = categories,
            LimitAmount = limit
        };
        return _budgetRepository.Insert(newBudget);
        
    }
}