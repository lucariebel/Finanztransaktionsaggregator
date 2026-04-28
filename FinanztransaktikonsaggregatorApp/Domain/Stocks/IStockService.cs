using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Stocks;

public interface IStockService
{
    List<Stock> GetAll();

    Stock? GetById(int id);

    Stock Insert(Stock stock);

    Stock Update(Stock stock);

    void Delete(Stock stock);
}