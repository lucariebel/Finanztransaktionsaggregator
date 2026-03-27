using FinanztransaktikonsaggregatorApp.Controllers.Helper;
using FinanztransaktikonsaggregatorApp.Domain.Imports;
using FinanztransaktikonsaggregatorApp.Filter;

namespace FinanztransaktikonsaggregatorApp.UI.Commands;

public class ExportCommand : ICommand
{

    private readonly IExportService _exportService;
    public string Name { get; } = "Export";
    public ExportCommand(IExportService exportService)
    {
        _exportService = exportService;
    }

    private void Export(Func<string, TransactionFilter, string> exportFunc)
    {
        int? year = null;
        int? month = null;
        int? day = null;

        string filepath = InputHelper.GetRequiredString("Please enter the file path:");

        List<int> accountNumbers = InputHelper.GetIntList("Enter Account Numbers (comma separated or leave empty for all):");

        List<string> categories = InputHelper.GetStringList("Enter Categories (comma separated or leave empty for all):");

        DateTime? startDate = InputHelper.GetDateTime("Enter an start date for an timespan or leave empty for no timespan (format: yyyy-mm-dd).");

        DateTime? endDate = InputHelper.GetDateTime("Enter an end date for an timespan or leave empty for no timespan (format: yyyy-mm-dd)."); ;
        if (!startDate.HasValue && !endDate.HasValue)
        {
            Console.WriteLine("Enter a year or leave empty for no year.");
            year = ParserHelper.ParseOptionalInt(Console.ReadLine(), 0, null, "Invalid year. Please enter a number above 0 (or Enter to skip):");

            Console.WriteLine("Enter a month to filter or leave empty for no month.");
            month = ParserHelper.ParseOptionalInt(Console.ReadLine(), 0, 12, "Invalid month. Please enter a number between 1 and 12 (or Enter to skip):");

            Console.WriteLine("Enter a day to filter or leave empty for no day.");
            day = ParserHelper.ParseOptionalInt(Console.ReadLine(), 0, 31, "Invalid day. Please enter a number between 1 and 31 (or Enter to skip):");
        }
        Console.WriteLine("Enter a min Amount or leave empty for no limit.");
        decimal? minAmount = ParserHelper.ParseOptinalDecimal(Console.ReadLine());

        Console.WriteLine("Enter a max Amount or leave empty for no limit.");
        decimal? maxAmount = ParserHelper.ParseOptinalDecimal(Console.ReadLine());

        Console.WriteLine("Enter keyword for search or leave empty for no search.");
        string descriptionContains = Console.ReadLine();
        var filter = new TransactionFilter
        {
            AccountNumbers = accountNumbers,
            Categories = categories,
            StartDate = startDate,
            EndDate = endDate,
            Year = year,
            Month = month,
            Day = day,
            MinAmount = minAmount,
            MaxAmount = maxAmount,
            DescriptionContains = descriptionContains,
        };
        Console.WriteLine("Start Export...");
        string resultMessage = exportFunc(filepath, filter);
        Console.WriteLine(resultMessage);
        Console.WriteLine($"Export finished. File saved at {filepath}. ");
        Console.WriteLine("Press Enter to return.");
        Console.ReadKey();
    }

    private void ExportTransactions()
    {
        Console.WriteLine("Would you like to export the transactions as a .csv or a .pdf file?");
        string fileFormat = InputHelper.GetRequiredString("Please enter the file format as csv or pdf:");
        fileFormat = fileFormat.ToLower();
        if(fileFormat == "pdf")
        {
            Export(_exportService.ExportTransactionsAsPDF);
        }
        else if(fileFormat == "csv")
        {
            Export(_exportService.ExportTransactionsAsCSV);
        }
    }

    public void Execute()
    {
        MenuHelper.CreateHeader("Export DATA");
        Console.WriteLine();

        ConsoleHelper.ConfirmAndExecute("You want to export Transactions?", 
            () => ExportTransactions());
    }
}