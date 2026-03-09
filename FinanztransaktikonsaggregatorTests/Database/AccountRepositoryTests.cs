using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Database
{
    public class AccountRepositoryTests : IDisposable
    {
        private readonly string _testDbPath;
        private readonly AppConfig _testConfig;
        private readonly DatabaseManager _dbManager;
        private readonly AccountRepository _repo;

        public AccountRepositoryTests()
        {
            _testDbPath = $"test_accounts_{Guid.NewGuid()}.db";

            _testConfig = new AppConfig(_testDbPath);

            _dbManager = new DatabaseManager(_testConfig);
            _dbManager.Initialize();

            _repo = new AccountRepository(_testConfig);
        }

        [Fact]
        public void Insert_And_GetAll()
        {
            // ARRANGE
            var newAccount = new Account
            {
                Name = "Girokonto",
                Institution = "Sparkasse",
                InitialBalance = 1500.00m
            };

            // ACT
            _repo.Insert(newAccount);
            var results = _repo.GetAll();

            // ASSERT
            Assert.Single(results);

            var savedAccount = results.First();
            
            Assert.True(savedAccount.Id > 0); 
            Assert.Equal("Girokonto", savedAccount.Name);
            Assert.Equal("Sparkasse", savedAccount.Institution);
            Assert.Equal(1500.00m, savedAccount.InitialBalance);
        }

        [Fact]
        public void Insert_And_UpdateAccount()
        {
            // ARRANGE
            var newAccount = new Account
            {
                Name = "Girokonto",
                Institution = "Sparkasse",
                InitialBalance = 1500.00m
            };

            // ACT
            _repo.Insert(newAccount);
            newAccount.InitialBalance = 2500.50m;
            _repo.Update(newAccount);
            
            var results = _repo.GetAll();

            // ASSERT
            Assert.Single(results);

            var savedAccount = results.First();
            
            Assert.Equal(1, savedAccount.Id); 
            Assert.Equal("Girokonto", savedAccount.Name);
            Assert.Equal("Sparkasse", savedAccount.Institution);
            Assert.Equal(2500.50m, savedAccount.InitialBalance);
        }

        [Fact]
        public void Insert_And_DeleteAccount()
        {
            // ARRANGE
            var newAccount = new Account
            {
                Name = "Girokonto",
                Institution = "Sparkasse",
                InitialBalance = 1500.00m
            };

            // ACT
            _repo.Insert(newAccount);
            _repo.Delete(newAccount);
            
            var results = _repo.GetAll();

            // ASSERT
            Assert.Empty(results);
        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (File.Exists(_testDbPath))
            {
                File.Delete(_testDbPath);
            }
        }
    }
}