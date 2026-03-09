namespace FinanztransaktikonsaggregatorApp.Entities;

public class Budget
{
    public int Id { get; set; }
    public string Category { get; set; }
    public decimal LimitAmount { get; set; }

    public override string ToString()
    {
        return $"Budget for {Category}: {LimitAmount:C2}";
    }
}