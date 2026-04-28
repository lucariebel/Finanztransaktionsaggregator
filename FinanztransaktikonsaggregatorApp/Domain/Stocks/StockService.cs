using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Domain.Stocks.Prices;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IStockPriceProvider _priceProvider;

    public StockService(IStockRepository stockRepository, IStockPriceProvider priceProvider)
    {
        _stockRepository = stockRepository;
        _priceProvider = priceProvider;
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

    public void UpdatePrices()
    {
        var stocks = _stockRepository.GetAll();

        foreach (var stock in stocks)
        {
            var quote = _priceProvider.GetPrice(stock.TickerSymbol);

            stock.PreviousKnownPrice = stock.LastKnownPrice;
            stock.PreviousUpdated = stock.LastUpdated;

            stock.LastKnownPrice = quote.Price;
            stock.LastUpdated = quote.Timestamp;

            _stockRepository.Update(stock);
        }
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