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

    private void AddBudget()
    {
        string category;
        decimal limit;

        Console.WriteLine("You chose Yes. Please enter the Category:");
        category = InputHelper.GetRequiredString("Please enter a category:");
        Console.WriteLine("Please enter the Limit:");
        string input = Console.ReadLine();
        limit = ParserHelper.ParseDecimal(input);
        var newBudget = _budgetService.AddNewBudget(category, limit);
        Console.WriteLine($"Budget succesfully added! ID: {newBudget.Id}, Kategorie: {newBudget.Category}, Limit: {newBudget.LimitAmount}");
        Console.WriteLine();
    }

    public void Execute()
    {
        bool addBudget = true;

        MenuHelper.CreateHeader("ADD BDUGET");
        Console.WriteLine();

        while (addBudget)
        {
            Console.WriteLine("You want to add a new Budget? \nPress y (Yes) Enter (No)");
            var key = Console.ReadKey(true).Key;
            Console.WriteLine();
            if (key == ConsoleKey.Y)
            {
                AddBudget();
            }
            else if (key == ConsoleKey.Enter)
            {
                addBudget = false;
            }
            else
            {
                Console.WriteLine("Invalid key!");
                Console.WriteLine();
            }
        }

    }
}