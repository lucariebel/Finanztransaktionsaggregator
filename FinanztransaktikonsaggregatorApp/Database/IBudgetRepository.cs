using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IBudgetRepository
{
    List<Budget> GetAll();
    Budget GetById(int id);
    void Insert(Budget budget);
    void Update(Budget budget);
    void Delete(Budget budget);
}