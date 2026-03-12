using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Entities;
using System.Collections.Generic;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class DeleteBudgetCommand : ICommand
{
    private readonly IBudgetService _budgetService;
    private List<Budget> _budgets;

    public DeleteBudgetCommand(IBudgetService budgetService)
    {
        _budgetService = budgetService;
        _budgets = _budgetService.GetAllBudgets();
    }
    public string Name { get; } = "Delete budget";

    public void Execute()
    {
        int id;
        bool deleteBudget = true;
        while (deleteBudget)
        {
            Console.Clear();
            MenuHelper.CreateHeader("DELETE BUDGET");
            Console.WriteLine();
            MenuHelper.BudgetList(3, _budgetService.GetAllBudgets());
            if (_budgets.Count > 0)
            {
                Console.WriteLine("Please enter the id you want to delete.");
                Console.WriteLine("Press 'n' to cancel.");
                string input = Console.ReadLine();
                Console.WriteLine();
                if((input.Any(char.IsLetter)))
                {
                    deleteBudget = false;
                }
                else
                {
                    id = InputHelper.GetValidId
                    (
                        "Invalid ID! Please enter a valid ID:",
                        _budgetService.IsValidId,
                        input
                    );
                    _budgetService.DeleteBudget(id);
                    Console.WriteLine("Budget deleted.");
                    _budgets = _budgetService.GetAllBudgets();
                }
            }
            else
            {
                Console.ReadKey();
                deleteBudget = false;
            }
        }
    }
}