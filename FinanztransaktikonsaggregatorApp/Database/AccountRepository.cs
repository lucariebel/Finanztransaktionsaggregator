using Microsoft.Data.Sqlite;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(AppConfig config) : base(config)
        {
        }

        public List<Account> GetAll()
        {
            var list = new List<Account>();

            using (var connection = GetOpenConnection())
            {
                string sql = "SELECT Id, Name, Institution, InitialBalance FROM Accounts";

                using (var command = new SqliteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Account
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Institution = reader.GetString(2),
                            InitialBalance = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return list;
        }

        public Account? GetById(int id)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = "SELECT Id, Name, Institution, InitialBalance FROM Accounts WHERE Id = @id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Account
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Institution = reader.IsDBNull(2) ? null : reader.GetString(2),
                                InitialBalance = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public Account Insert(Account account)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = @"
                    INSERT INTO Accounts (Name, Institution, InitialBalance) 
                    VALUES (@name, @institution, @initialBalance)
                    RETURNING Id;";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", account.Name);
                    command.Parameters.AddWithValue("@institution", account.Institution);
                    command.Parameters.AddWithValue("@initialBalance", account.InitialBalance);

                    long newId = (long)command.ExecuteScalar();

                    account.Id = (int)newId;
                }
            }

            return account;
        }

        public Account Update(Account account)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = @"
                    UPDATE Accounts 
                    SET Id = @id, 
                        Name = @name, 
                        Institution = @institution, 
                        InitialBalance = @initialBalance 
                    WHERE Id = @id
                    RETURNING Id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", account.Id);
                    command.Parameters.AddWithValue("@name", account.Name);
                    command.Parameters.AddWithValue("@institution", account.Institution);
                    command.Parameters.AddWithValue("@initialBalance", account.InitialBalance);

                    long newId = (long)command.ExecuteScalar();
            
                    account.Id = (int)newId;
                }
            }

            return account;
        }

        public void Delete(Account account)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = "DELETE FROM Accounts WHERE Id = @id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", account.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}