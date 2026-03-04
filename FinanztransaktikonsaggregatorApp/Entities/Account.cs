namespace FinanztransaktikonsaggregatorApp.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Institution { get; set; }
    public decimal InitialBalance { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Institution}) - {Id}";
    }
}