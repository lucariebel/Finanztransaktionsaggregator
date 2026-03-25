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

    public decimal GetUsedBudget(string category, int year, int month)
    {

        decimal usedBudget = _transactionService.GetUsedBudgetByCategoryAndMonth(category, year, month);

        return Math.Abs(usedBudget);
    }

    public decimal GetIncome(string category, int year, int month)
    {
        return _transactionService.GetIncomeByCategory(category, year, month);
    }

    public decimal CalculateRest(decimal budget, string category, int year, int month)
    {
        decimal used = GetUsedBudget(category, year, month);
        decimal rest = budget + GetIncome(category, year, month) - used;
        return rest;
    }

    public decimal CalculatePercentage(decimal budget, string category, int year, int month)
    {
        decimal used = GetUsedBudget(category, year, month);
        decimal percentage = used / budget;
        return percentage * 100;
    }

    public Budget AddNewBudget(string category, decimal limit)
    {
        Budget newBudget = new Budget
        {
            Category = category,
            LimitAmount = limit
        };
        return _budgetRepository.Insert(newBudget);
        
    }

    public void DeleteBudget(int id)
    {
        _budgetRepository.Delete(id);
    }
    public Budget UpdateBudget(int id, string category, decimal limit)
    {
        Budget updateBudget = new Budget
        {
            Id = id,
            Category = category,
            LimitAmount = limit
        };
        return _budgetRepository.Update(updateBudget);
    }

    public bool IsValidId(int id)
    {
        var budget = GetAllBudgets().FirstOrDefault(budget => budget.Id == id);

        if (budget == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}