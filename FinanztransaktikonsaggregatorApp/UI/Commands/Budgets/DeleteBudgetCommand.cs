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

    private void BugdetList()
    {
        int cols = 3;
        _budgets = _budgetService.GetAllBudgets();
        Console.WriteLine("Your Budgets:");
        MenuHelper.CreateHorizontalLine();

        Console.WriteLine(MenuHelper.TableFormatterBudget(cols), "Category", "ID", "Limit");
        if (_budgets.Count == 0)
        {
            Console.WriteLine("No budgets have been defined.");
        }
        else
        {
            foreach (var budget in _budgets)
            {
                Console.WriteLine(MenuHelper.TableFormatterBudget(cols), $"{budget.Category}", budget.Id, $"{budget.LimitAmount:C2}");
            }
        }

    }


    public void Execute()
    {
        int id;
        bool deleteBudget = true;
        while (deleteBudget)
        {
            Console.Clear();
            MenuHelper.CreateHeader("DELETE BUDGET");
            Console.WriteLine();
            BugdetList();
            if (_budgets.Count > 0)
            {
                Console.WriteLine("Please enter the id you want to delete.");
                Console.WriteLine("Press 'n' to cancel.");
                string input = Console.ReadLine();
                Console.WriteLine();
                if (input.ToLower() == "n")
                {
                    deleteBudget = false;
                }
                else if((input.Any(char.IsLetter)))
                {
                    deleteBudget = false;
                }
                else
                {
                    id = ParserHelper.ParseInteger(input);
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