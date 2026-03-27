namespace FinanztransaktikonsaggregatorApp.Filter
{
    public class TransactionFilter
    {
        public List<int>? AccountNumbers { get; set; }
        public List<string>? Categories { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? DescriptionContains { get; set; }
    }
}
