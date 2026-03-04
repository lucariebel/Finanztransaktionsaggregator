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
    
    public void Insert(Transaction transaction)
    {
        using (var connection = GetOpenConnection())
        {
            string sql = @"
                    INSERT INTO Transactions (Date, Amount, Description, Category, AccountNumber) 
                    VALUES (@date, @amount, @description, @category, @accountNumber)";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@date", transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@amount", transaction.Amount);
                command.Parameters.AddWithValue("@description", transaction.Description ?? string.Empty);
                command.Parameters.AddWithValue("@category", transaction.Category ?? "Uncategorized");
                command.Parameters.AddWithValue("@accountNumber", transaction.AccountNumber);

                command.ExecuteNonQuery();
            }
        }
    }
}