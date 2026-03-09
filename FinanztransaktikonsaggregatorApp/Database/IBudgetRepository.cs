using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IBudgetRepository
{
    List<Budget> GetAll();
    Budget GetById(int id);
    Budget Insert(Budget budget);
    Budget Update(Budget budget);
    void Delete(Budget budget);
}