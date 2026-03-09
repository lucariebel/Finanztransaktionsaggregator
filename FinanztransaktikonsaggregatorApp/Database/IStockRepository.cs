using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IStockRepository
{
    List<Stock> GetAll();
    Stock GetById(int id);
    Stock Insert(Stock stock);
    Stock Update(Stock stock);
    void Delete(Stock stock);
}