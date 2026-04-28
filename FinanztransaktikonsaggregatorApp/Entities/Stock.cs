namespace FinanztransaktikonsaggregatorApp.Entities;

public class Stock
{
    public int Id { get; set; }
    public string TickerSymbol { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public decimal? LastKnownPrice { get; set; }
    public DateTime? LastUpdated { get; set; }
    public decimal? PreviousKnownPrice { get; set; }
    public DateTime? PreviousUpdated { get; set; }

    public decimal GetCurrentValue()
    {
        if (LastKnownPrice.HasValue) return Quantity * LastKnownPrice.Value;

        return Quantity * AverageBuyPrice;
    }

    public decimal GetProfitOrLoss()
    {
        if (LastKnownPrice.HasValue)
        {
            var totalInvested = Quantity * AverageBuyPrice;
            var currentValue = GetCurrentValue();
            return currentValue - totalInvested;
        }

        return 0m;
    }

    public decimal GetValueChangeSinceLastUpdate()
    {
        if (!LastKnownPrice.HasValue || !PreviousKnownPrice.HasValue)
        {
            return 0m;
        }

        return (LastKnownPrice.Value - PreviousKnownPrice.Value) * Quantity;
    }

    public decimal GetPriceChangeSinceLastUpdate()
    {
        if (!LastKnownPrice.HasValue || !PreviousKnownPrice.HasValue)
        {
            return 0m;
        }

        return LastKnownPrice.Value - PreviousKnownPrice.Value;
    }

    public decimal GetPriceChangePercentageSinceLastUpdate()
    {
        if (!LastKnownPrice.HasValue || !PreviousKnownPrice.HasValue)
        {
            return 0m;
        }

        if (PreviousKnownPrice.Value == 0m)
        {
            return 0m;
        }

        return (LastKnownPrice.Value - PreviousKnownPrice.Value) / PreviousKnownPrice.Value * 100m;
    }
}