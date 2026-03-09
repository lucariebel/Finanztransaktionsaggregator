using FinanztransaktikonsaggregatorApp.Entities;
using Microsoft.Data.Sqlite;

namespace FinanztransaktikonsaggregatorApp.Database;

public class BudgetRepository : BaseRepository, IBudgetRepository
{
    public BudgetRepository(AppConfig config) : base(config)
    {
    }

    public List<Budget> GetAll()
    {
        var list = new List<Budget>();

        using (var connection = GetOpenConnection())
        {
            var sql = "SELECT Id, Category, LimitAmount FROM Budgets";

            using (var command = new SqliteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    list.Add(new Budget
                    {
                        Id = reader.GetInt32(0),
                        Category = reader.GetString(1),
                        LimitAmount = reader.GetDecimal(2)
                    });
            }
        }

        return list;
    }

    public Budget GetById(int id)
    {
        using (var connection = GetOpenConnection())
        {
            var sql = "SELECT Id, Category, LimitAmount FROM Budgets WHERE Id = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return new Budget
                        {
                            Id = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            LimitAmount = reader.GetDecimal(2)
                        };
                }
            }
        }

        return null;
    }

    public Budget Insert(Budget budget)
    {
        using (var connection = GetOpenConnection())
        {
            var sql = @"
                    INSERT INTO Budgets (Category, LimitAmount) 
                    VALUES (@category, @limitAmount)
                    RETURNING Id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@category", budget.Category);
                command.Parameters.AddWithValue("@limitAmount", budget.LimitAmount);

                var newId = (long)command.ExecuteScalar();

                budget.Id = (int)newId;
            }
        }

        return budget;
    }

    public Budget Update(Budget budget)
    {
        using (var connection = GetOpenConnection())
        {
            var sql = @"
                    UPDATE Budgets 
                    SET Category = @category, 
                        LimitAmount = @limitAmount 
                    WHERE Id = @id
                    RETURNING Id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", budget.Id);
                command.Parameters.AddWithValue("@category", budget.Category);
                command.Parameters.AddWithValue("@limitAmount", budget.LimitAmount);

                var newId = (long)command.ExecuteScalar();

                budget.Id = (int)newId;
            }
        }

        return budget;
    }

    public void Delete(Budget budget)
    {
        using (var connection = GetOpenConnection())
        {
            var sql = "DELETE FROM Budgets WHERE Id = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", budget.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}