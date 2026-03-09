using FinanztransaktikonsaggregatorApp.Entities;
using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorApp.Database;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(AppConfig config) : base(config)
    {
    }

    public List<Transaction> GetAll()
    {
        var list = new List<Transaction>();

        using (var connection = GetOpenConnection())
        {
            string sql = "SELECT Date, Amount, Description, Category, AccountNumber FROM Transactions";

            using (var command = new SqliteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Transaction
                    {
                        Date = DateTime.Parse(reader.GetString(0)),
                        Amount = reader.GetDecimal(1),
                        Description = reader.GetString(2),
                        Category = reader.GetString(3),
                        AccountNumber = reader.GetInt32(4)
                    });
                }
            }
        }
        return list;
    }
    
    public Transaction Insert(Transaction transaction)
    {
        using (var connection = GetOpenConnection())
        {
            string sql = @"
                    INSERT INTO Transactions (Date, Amount, Description, Category, AccountNumber) 
                    VALUES (@date, @amount, @description, @category, @accountNumber)
                    RETURNING Id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@date", transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@amount", transaction.Amount);
                command.Parameters.AddWithValue("@description", transaction.Description ?? string.Empty);
                command.Parameters.AddWithValue("@category", transaction.Category ?? "Uncategorized");
                command.Parameters.AddWithValue("@accountNumber", transaction.AccountNumber);

                long newId = (long)command.ExecuteScalar();
            
                transaction.Id = (int)newId;
            }
        }

        return transaction;
    }

    public List<Transaction> GetTransactionsByMonth(int year, int month)
    {
        var list = new List<Transaction>();

        using (var connection = GetOpenConnection()) 
        {
            string sql = @"
            SELECT Id, Date, Amount, Description, Category, AccountNumber 
            FROM Transactions 
            WHERE strftime('%Y', Date) = @year 
              AND strftime('%m', Date) = @month";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@year", year.ToString("D4"));
                command.Parameters.AddWithValue("@month", month.ToString("D2"));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Transaction
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.Parse(reader.GetString(1)),
                            Amount = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Category = reader.IsDBNull(4) ? "Uncategorized" : reader.GetString(4),
                            AccountNumber = reader.GetInt32(5)
                        });
                    }
                }
            }
        }
        return list;
    }

    public Transaction Update(Transaction transaction)
    {
        using (var connection = GetOpenConnection())
        {
            string sql = @"
                UPDATE Transactions 
                SET Date = @date, 
                    Amount = @amount, 
                    Description = @description, 
                    Category = @category, 
                    AccountNumber = @accountNumber 
                WHERE Id = @id
                RETURNING Id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", transaction.Id);
                command.Parameters.AddWithValue("@date", transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@amount", transaction.Amount);
                command.Parameters.AddWithValue("@description", transaction.Description ?? string.Empty);
                command.Parameters.AddWithValue("@category", transaction.Category ?? "Uncategorized");
                command.Parameters.AddWithValue("@accountNumber", transaction.AccountNumber);

                command.ExecuteNonQuery();
            }
        }

        return transaction;
    }

    public void Delete(Transaction transaction)
    {
        using (var connection = GetOpenConnection())
        {
            string sql = "DELETE FROM Transactions WHERE Id = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", transaction.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}