using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;
using System.Collections.Generic;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class AddNewBudgetCommand : ICommand
{
    private readonly IBudgetService _budgetService;

    public AddNewBudgetCommand(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }
    public string Name { get; } = "Add new budget";

    public void Execute()
    {
        MenuHelper.CreateHeader("ADD BDUGET");
        Console.WriteLine();
        bool addBudget = true;
        while (addBudget)
        {
            Console.WriteLine("You wanna add a new Budget? \nPress j (Yes) n (No)");
            var key = Console.ReadKey(true).Key;
            Console.WriteLine();
            if (key == ConsoleKey.J)
            {
                Console.WriteLine("You chose Yes. Please enter the Category:");
                string categorie = Console.ReadLine();
                Console.WriteLine("Please enter the Limit:");
                decimal limit;
                string input = Console.ReadLine();
                while (!decimal.TryParse(input, out limit) || limit < 0)
                {
                    Console.WriteLine("Invalid number. Please enter a valid positive number:");
                    input = Console.ReadLine();
                }
                var newBudget = _budgetService.AddNewBudget(categorie, limit);
                Console.WriteLine($"Budget erfolgreich hinzugefügt! ID: {newBudget.Id}, Kategorie: {newBudget.Category}, Limit: {newBudget.LimitAmount}");
                Console.WriteLine();
            }
            else if (key == ConsoleKey.N)
            {
                Console.WriteLine("You chose No. Aborting.\nPress Enter to Return");
                addBudget = false;
            }
        }

    }
}