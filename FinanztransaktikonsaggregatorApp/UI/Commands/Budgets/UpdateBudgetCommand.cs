using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Budgets;

namespace FinanztransaktikonsaggregatorApp.UI.Commands.Budgets;

public class UpdateBudgetCommand : ICommand
{
    public string Name { get; } = "Update Budgets";
    public void Execute()
    {

    }
}