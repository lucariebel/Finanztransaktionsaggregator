using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class UpdateBudgetCommand : ICommand
{
    private readonly IBudgetService _budgetService;
    private List<Budget> _budgets;
    public UpdateBudgetCommand(IBudgetService budgetService)
    {
        _budgetService = budgetService;
        _budgets = _budgetService.GetAllBudgets();
    }
    public string Name { get; } = "Update Budgets";

    private void UpdateBudget(string input)
    {
        int id = InputHelper.GetValidId("Invalid ID! Please enter a valid ID:", _budgetService.IsValidId, input);

        string category = InputHelper.GetRequiredString("Please enter a category:");

        decimal limit = InputHelper.GetRequiredDecimal("Enter new limit:");

        var updatedBudget = _budgetService.UpdateBudget(id, category, limit);

        Console.WriteLine($"Budget successfully updated: ID {updatedBudget.Id}, Category {updatedBudget.Category}, Limit {updatedBudget.LimitAmount:C2}");
        Console.WriteLine();
    }

    public void Execute()
    {
        bool updateBudget = true;

        while (updateBudget)
        {
            Console.Clear();
            MenuHelper.CreateHeader("UPDATE BUDGET");
            Console.WriteLine();

            _budgets = _budgetService.GetAllBudgets();
            MenuHelper.BudgetList(3, _budgets);

            if (_budgets.Count > 0)
            {
                Console.WriteLine("Enter the ID you want to update.");
                Console.WriteLine("Press 'Enter' to cancel.");

                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    updateBudget = false;
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
                    UpdateBudget(input);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Press key to return");
                Console.ReadLine();
                updateBudget = false;
            }
        }
    }
}