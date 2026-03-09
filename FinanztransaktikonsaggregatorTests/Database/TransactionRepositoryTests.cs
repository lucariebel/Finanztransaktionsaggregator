using FinanztransaktikonsaggregatorApp;
using FinanztransaktikonsaggregatorApp.Database;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorTests.Database
{
    public class TransactionRepositoryTests : IDisposable
    {
        private readonly string _testDbPath;
        private readonly AppConfig _testConfig;
        private readonly DatabaseManager _dbManager;
        private readonly TransactionRepository _repo;

        public TransactionRepositoryTests()
        {
            _testDbPath = $"test_transactions_{Guid.NewGuid()}.db";

            _testConfig = new AppConfig(_testDbPath);

            _dbManager = new DatabaseManager(_testConfig);
            _dbManager.Initialize();

            _repo = new TransactionRepository(_testConfig);
        }

        [Fact]
        public void Insert_And_GetAll()
        {
            // ARRANGE
            var transaction = new Transaction
            {
                Date = new DateTime(2026, 3, 9),
                Amount = -45.50m,
                Description = "Wocheneinkauf",
                Category = "Lebensmittel",
                AccountNumber = 1
            };

            // ACT
            _repo.Insert(transaction);
            var results = _repo.GetAll();

            // ASSERT
            Assert.Single(results);
            var saved = results.First();
            Assert.Equal("Wocheneinkauf", saved.Description);
            Assert.Equal(-45.50m, saved.Amount);
            Assert.Equal(1, saved.AccountNumber);
        }

        [Fact]
        public void Insert_And_UpdateTransaction()
        {
            // ARRANGE
            var transaction = new Transaction
            {
                Date = new DateTime(2026, 3, 9),
                Amount = -45.50m,
                Description = "Wocheneinkauf",
                Category = "Lebensmittel",
                AccountNumber = 1
            };

            // ACT
            _repo.Insert(transaction);
            transaction.Amount = -50.00m;
            transaction.Description = "Wocheneinkauf Korrektur";
            _repo.Update(transaction);

            var results = _repo.GetAll();

            // ASSERT
            Assert.Single(results);
            var saved = results.First();
            Assert.Equal(-50.00m, saved.Amount);
            Assert.Equal("Wocheneinkauf Korrektur", saved.Description);
        }

        [Fact]
        public void Insert_And_DeleteTransaction()
        {
            // ARRANGE
            var transaction = new Transaction
            {
                Date = new DateTime(2026, 3, 9),
                Amount = -45.50m,
                Description = "Wocheneinkauf",
                Category = "Lebensmittel",
                AccountNumber = 1
            };

            // ACT
            _repo.Insert(transaction);
            _repo.Delete(transaction);
            var results = _repo.GetAll();

            // ASSERT
            Assert.Empty(results);
        }

        [Fact]
        public void GetTransactionsByMonth_ShouldFilterCorrectly()
        {
            // ARRANGE
            _repo.Insert(new Transaction { Date = new DateTime(2026, 3, 1), Amount = -10, AccountNumber = 1 });
            _repo.Insert(new Transaction { Date = new DateTime(2026, 3, 15), Amount = -20, AccountNumber = 1 });
            _repo.Insert(new Transaction { Date = new DateTime(2026, 4, 1), Amount = -30, AccountNumber = 1 });

            // ACT
            var marchTransactions = _repo.GetTransactionsByMonth(2026, 3);

            // ASSERT
            Assert.Equal(2, marchTransactions.Count);
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