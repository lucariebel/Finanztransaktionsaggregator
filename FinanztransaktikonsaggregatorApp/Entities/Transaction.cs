namespace FinanztransaktikonsaggregatorApp.Entities;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; } = "Uncategorized";
    public int AccountNumber { get; set; }
    
    public override string ToString()
    {
        return $"[{Date:yyyy-MM-dd}] {Amount,15:C2} | {Category,-15} | {Description}";
    }
}