namespace FinanztransaktikonsaggregatorApp.Filter;

public class TransactionSortFilter
{
    public TransactionSortBy SortBy { get; set; } = TransactionSortBy.None;
    public bool Descending { get; set; }
}

public enum TransactionSortBy
{
    None,
    Date,
    Amount,
    Category,
    Account
}
