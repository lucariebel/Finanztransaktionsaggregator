namespace FinanztransaktikonsaggregatorApp.Entities;

public class Stock
{
    public string TickerSymbol { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public decimal? LastKnownPrice { get; set; }
    public DateTime? LastUpdated { get; set; }
    public decimal GetCurrentValue()
    {
        if (LastKnownPrice.HasValue)
        {
            return Quantity * LastKnownPrice.Value;
        }
        return Quantity * AverageBuyPrice; 
    }

    public decimal GetProfitOrLoss()
    {
        if (LastKnownPrice.HasValue)
        {
            decimal totalInvested = Quantity * AverageBuyPrice;
            decimal currentValue = GetCurrentValue();
            return currentValue - totalInvested;
        }
        return 0m;
    }
}