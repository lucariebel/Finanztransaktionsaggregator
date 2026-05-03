using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Mocks;

class FakeStockRepository : IStockRepository
{
    private readonly List<Stock> _stocks;
    private int _idCounter = 1;

    public FakeStockRepository()
    {
        _stocks = new List<Stock>();
    }

    public FakeStockRepository(List<Stock> stocks)
    {
        _stocks = stocks;
    }

    public List<Stock> GetAll()
    {
        return _stocks;
    }

    public Stock GetById(int id)
    {
        return _stocks.FirstOrDefault(stock => stock.Id == id);
    }

    public Stock Insert(Stock stock)
    {
        stock.Id = _idCounter++;
        _stocks.Add(stock);
        return stock;
    }

    public Stock Update(Stock stock)
    {
        var existingStock = _stocks.FirstOrDefault(item => item.Id == stock.Id);

        if (existingStock == null)
            return null;

        existingStock.TickerSymbol = stock.TickerSymbol;
        existingStock.Name = stock.Name;
        existingStock.Quantity = stock.Quantity;
        existingStock.AverageBuyPrice = stock.AverageBuyPrice;
        existingStock.LastKnownPrice = stock.LastKnownPrice;
        existingStock.LastUpdated = stock.LastUpdated;
        existingStock.PreviousKnownPrice = stock.PreviousKnownPrice;
        existingStock.PreviousUpdated = stock.PreviousUpdated;

        return existingStock;
    }

    public void Delete(Stock stock)
    {
        _stocks.Remove(stock);
    }
}