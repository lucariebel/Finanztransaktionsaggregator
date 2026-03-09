using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database;

public interface IAccountRepository
{
    List<Account> GetAll();
    Account? GetById(int id);
    Account Insert(Account account);
    Account Update(Account account);
    void Delete(Account account);
}