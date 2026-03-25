using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Accounts;

public interface IAccountService
{
    List<Account> GetAll();
    Account? GetById(int id);
    Account Insert(Account account);
    Account Update(Account account);
    void Delete(Account account);
}
