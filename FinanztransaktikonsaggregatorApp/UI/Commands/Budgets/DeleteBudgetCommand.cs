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
            _budgets = _budgetService.GetAllBudgets();
            MenuHelper.BudgetList(3, _budgets);
            if (_budgets.Count > 0)
            {

                Console.WriteLine("Please enter the id you want to delete.");
                Console.WriteLine("Press 'Enter' to cancel.");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    deleteBudget = false;
                }
                else if (input.Any(char.IsLetter))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid key!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
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
                Console.WriteLine();
                Console.WriteLine("Press key to return");
                Console.ReadLine();
                deleteBudget = false;
            }

        }
    }
}