using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IStockRepository
{
    List<Stock> GetAll();
    Stock GetById(int id);
    void Insert(Stock stock);
    void Update(Stock stock);
    void Delete(Stock stock);
}