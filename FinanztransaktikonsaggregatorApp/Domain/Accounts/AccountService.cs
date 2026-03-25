using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Domain.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public List<Account> GetAll() => _accountRepository.GetAll();

    public Account? GetById(int id) => _accountRepository.GetById(id);

    public Account Insert(Account account) => _accountRepository.Insert(account);

    public Account Update(Account account) => _accountRepository.Update(account);

    public void Delete(Account account) => _accountRepository.Delete(account);
}
