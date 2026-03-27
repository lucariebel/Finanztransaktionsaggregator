using FinanztransaktikonsaggregatorApp.Entities;
using FinanztransaktikonsaggregatorApp.Filter;
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
            var sql = "SELECT Date, Amount, Description, Category, AccountNumber FROM Transactions";

            using (var command = new SqliteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
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

        return list;
    }

    public List<Transaction> GetTransactionSpendingsByCategoryAndMonth(string category, int year, int month)
    {
        var list = new List<Transaction>();
        using (var connection = GetOpenConnection())
        {
        var sql = @"
            SELECT Amount, Date 
            FROM Transactions 
            WHERE Category = @category
              AND strftime('%Y', Date) = @year
              AND strftime('%m', Date) = @month";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@year", year.ToString("D4"));
                command.Parameters.AddWithValue("@month", month.ToString("D2"));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(new Transaction
                        {
                            Amount = reader.GetDecimal(0),

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
            var sql = @"
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

                var newId = (long)command.ExecuteScalar();

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
            var sql = @"
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
                        list.Add(new Transaction
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.Parse(reader.GetString(1)),
                            Amount = reader.GetDecimal(2),
                            Description = reader.GetString(3),
                            Category = reader.GetString(4),
                            AccountNumber = reader.GetInt32(5)
                        });
                }
            }
        }

        return list;
    }

    public Transaction Update(Transaction transaction)
    {
        using (var connection = GetOpenConnection())
        {
            var sql = @"
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
            var sql = "DELETE FROM Transactions WHERE Id = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", transaction.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Transaction> GetFiltered(TransactionFilter filter)
    {
        var list = new List<Transaction>();
        var conditions = new List<string>();

        using var connection = GetOpenConnection();
        string sql = "SELECT ID, Date, Amount, Description, Category, AccountNumber FROM Transactions";
        var command = new SqliteCommand(sql, connection);

        if (filter.AccountNumbers != null && filter.AccountNumbers.Any())
        {
            var paramNames = filter.AccountNumbers.Select((_, i) => $"@acc{i}").ToArray();
            conditions.Add($"AccountNumber IN ({string.Join(",", paramNames)})");

            for (int i = 0; i < filter.AccountNumbers.Count; i++)
                command.Parameters.AddWithValue(paramNames[i], filter.AccountNumbers[i]);
        }

        // Categories
        if (filter.Categories != null && filter.Categories.Any())
        {
            var paramNames = filter.Categories.Select((_, i) => $"@cat{i}").ToArray();
            conditions.Add($"Category IN ({string.Join(",", paramNames)})");

            for (int i = 0; i < filter.Categories.Count; i++)
                command.Parameters.AddWithValue(paramNames[i], filter.Categories[i]);
        }

        // Datum
        if (filter.StartDate.HasValue)
        {
            conditions.Add("Date >= @startDate");
            command.Parameters.AddWithValue("@startDate", filter.StartDate.Value.ToString("yyyy-MM-dd"));
        }
        if (filter.EndDate.HasValue)
        {
            conditions.Add("Date <= @endDate");
            command.Parameters.AddWithValue("@endDate", filter.EndDate.Value.ToString("yyyy-MM-dd"));
        }

        // Jahr / Monat / Tag (falls kein Datumsbereich angegeben)
        if (!filter.StartDate.HasValue && !filter.EndDate.HasValue)
        {
            if (filter.Year.HasValue)
            {
                conditions.Add("strftime('%Y', Date) = @year");
                command.Parameters.AddWithValue("@year", filter.Year.Value.ToString());
            }
            if (filter.Month.HasValue)
            {
                conditions.Add("strftime('%m', Date) = @month");
                command.Parameters.AddWithValue("@month", filter.Month.Value.ToString("D2"));
            }
            if (filter.Day.HasValue)
            {
                conditions.Add("strftime('%d', Date) = @day");
                command.Parameters.AddWithValue("@day", filter.Day.Value.ToString("D2"));
            }
        }

        // Optional: Amount / Description
        if (filter.MinAmount.HasValue)
        {
            conditions.Add("Amount >= @minAmount");
            command.Parameters.AddWithValue("@minAmount", filter.MinAmount.Value);
        }
        if (filter.MaxAmount.HasValue)
        {
            conditions.Add("Amount <= @maxAmount");
            command.Parameters.AddWithValue("@maxAmount", filter.MaxAmount.Value);
        }
        if (!string.IsNullOrWhiteSpace(filter.DescriptionContains))
        {
            conditions.Add("Description LIKE @desc");
            command.Parameters.AddWithValue("@desc", $"%{filter.DescriptionContains}%");
        }

        // SQL zusammenbauen

        if (conditions.Any())
            sql += " WHERE " + string.Join(" AND ", conditions);

        command.CommandText = sql;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Transaction
            {
                Id = reader.GetInt32(0),
                Date = DateTime.Parse(reader.GetString(1)),
                Amount = reader.GetDecimal(2),
                Description = reader.GetString(3),
                Category = reader.GetString(4),
                AccountNumber = reader.GetInt32(5)
            });
        }

        return list;
    }
}