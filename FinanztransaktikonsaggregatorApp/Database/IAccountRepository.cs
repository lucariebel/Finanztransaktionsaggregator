using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IAccountRepository
{
    List<Account> GetAll();
    Account GetById(int id);
    void Insert(Account account);
    void Update(Account account);
    void Delete(Account account);
}