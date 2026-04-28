using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public List<Stock> GetAll()
    {
        return _stockRepository.GetAll();
    }

    public Stock? GetById(int id)
    {
        return _stockRepository.GetById(id);
    }

    public Stock Insert(Stock stock)
    {
        PrepareStockForSaving(stock);
        return _stockRepository.Insert(stock);
    }

    public Stock Update(Stock stock)
    {
        PrepareStockForSaving(stock);
        return _stockRepository.Update(stock);
    }

    public void Delete(Stock stock)
    {
        _stockRepository.Delete(stock);
    }

    private static void PrepareStockForSaving(Stock stock)
    {
        stock.TickerSymbol = stock.TickerSymbol.Trim().ToUpperInvariant();
        stock.Name = stock.Name.Trim();

        if (!stock.LastKnownPrice.HasValue)
        {
            stock.LastKnownPrice = stock.AverageBuyPrice;
        }

        if (!stock.LastUpdated.HasValue)
        {
            stock.LastUpdated = DateTime.Now;
        }
    }
}