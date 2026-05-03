using FinanztransaktikonsaggregatorApp.Domain.Transactions;
using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Text;

namespace FinanztransaktikonsaggregatorApp.Domain.Imports;

public class ExportService : IExportService
{
    private readonly ITransactionService _transactionService;

    public ExportService(ITransactionService transactionService)
    {
        _transactionService = transactionService;

    }

    public string ExportTransactionsAsCSV(string filepath, TransactionsFilter transactionFilter)
    {
        List<Transaction> transactions =_transactionService.GetFilteredTransactions(transactionFilter);

        filepath = Path.ChangeExtension(filepath, ".csv");

        using (var writer = new StreamWriter(filepath, false, Encoding.UTF8))
        {
            writer.WriteLine("Date;Amount;Description;Category;AccountNumber");
            foreach(var transaction in transactions)
            {
                writer.WriteLine($"{transaction.Date:dd.MM.yyyy};{transaction.Amount:0.00};{transaction.Description};{transaction.Category};{transaction.AccountNumber}");
            }
        }
        return "Succesfully exported";
    }

    public string ExportTransactionsAsPDF(string filepath, TransactionsFilter transactionFilter)
    {
        List<Transaction> transactions = _transactionService.GetFilteredTransactions(transactionFilter);
        string extension = Path.GetExtension(filepath);

        filepath = Path.ChangeExtension(filepath, ".pdf");

        QuestPDF.Settings.License = LicenseType.Community;
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .Text("Transactions Export")
                    .FontSize(20)
                    .Bold();

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);  
                        columns.ConstantColumn(80);  
                        columns.ConstantColumn(60);  
                        columns.RelativeColumn();    
                        columns.RelativeColumn();    
                        columns.ConstantColumn(80);
                    });
                    table.Header(header =>
                    {
                        header.Cell().Text("Id").Bold();
                        header.Cell().Text("Date").Bold();
                        header.Cell().Text("Amount").Bold();
                        header.Cell().Text("Description").Bold();
                        header.Cell().Text("Category").Bold();
                        header.Cell().Text("AccountNumber").Bold();
                    });

                    foreach (var transaction in transactions)
                    {
                        table.Cell().Text(transaction.Id.ToString());
                        table.Cell().Text(transaction.Date.ToString("yyyy-MM-dd"));
                        table.Cell().Text(transaction.Amount.ToString("C", CultureInfo.GetCultureInfo("de-DE")));
                        table.Cell().Text(transaction.Description).WrapAnywhere();
                        table.Cell().Text(transaction.Category);
                        table.Cell().Text(transaction.AccountNumber.ToString());
                    }
                });
            });
        })
        .GeneratePdf(filepath);
        return "Succesfully exported PDF!";
    }
}