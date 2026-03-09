using Microsoft.Data.Sqlite;
using FinanztransaktikonsaggregatorApp.Entities;

namespace FinanztransaktikonsaggregatorApp.Database
{
    public class StockRepository : BaseRepository, IStockRepository
    {
        public StockRepository(AppConfig config) : base(config)
        {
        }

        public List<Stock> GetAll()
        {
            var list = new List<Stock>();

            using (var connection = GetOpenConnection())
            {
                string sql = "SELECT Id, TickerSymbol, Name, Quantity, AverageBuyPrice, LastKnownPrice, LastUpdated FROM Stocks";

                using (var command = new SqliteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Stock
                        {
                            Id = reader.GetInt32(0),
                            TickerSymbol = reader.GetString(1),
                            Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Quantity = reader.GetDecimal(3),
                            AverageBuyPrice = reader.GetDecimal(4),
                            LastKnownPrice = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                            LastUpdated = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6))
                        });
                    }
                }
            }
            return list;
        }

        public Stock GetById(int id)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = "SELECT Id, TickerSymbol, Name, Quantity, AverageBuyPrice, LastKnownPrice, LastUpdated FROM Stocks WHERE Id = @id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Stock
                            {
                                Id = reader.GetInt32(0),
                                TickerSymbol = reader.GetString(1),
                                Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Quantity = reader.GetDecimal(3),
                                AverageBuyPrice = reader.GetDecimal(4),
                                LastKnownPrice = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                                LastUpdated = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public Stock Insert(Stock stock)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = @"
                    INSERT INTO Stocks (TickerSymbol, Name, Quantity, AverageBuyPrice, LastKnownPrice, LastUpdated) 
                    VALUES (@tickerSymbol, @name, @quantity, @averageBuyPrice, @lastKnownPrice, @lastUpdated)
                    RETURNING Id;";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tickerSymbol", stock.TickerSymbol);
                    command.Parameters.AddWithValue("@name", stock.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@quantity", stock.Quantity);
                    command.Parameters.AddWithValue("@averageBuyPrice", stock.AverageBuyPrice);
                    
                    command.Parameters.AddWithValue("@lastKnownPrice", stock.LastKnownPrice.HasValue ? stock.LastKnownPrice.Value : (object)DBNull.Value);
                    
                    command.Parameters.AddWithValue("@lastUpdated", stock.LastUpdated.HasValue ? stock.LastUpdated.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);

                    long newId = (long)command.ExecuteScalar();
            
                    stock.Id = (int)newId;
                }
            }

            return stock;
        }

        public Stock Update(Stock stock)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = @"
                    UPDATE Stocks 
                    SET TickerSymbol = @tickerSymbol, 
                        Name = @name, 
                        Quantity = @quantity, 
                        AverageBuyPrice = @averageBuyPrice, 
                        LastKnownPrice = @lastKnownPrice, 
                        LastUpdated = @lastUpdated 
                    WHERE Id = @id
                    RETURNING Id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", stock.Id);
                    command.Parameters.AddWithValue("@tickerSymbol", stock.TickerSymbol);
                    command.Parameters.AddWithValue("@name", stock.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@quantity", stock.Quantity);
                    command.Parameters.AddWithValue("@averageBuyPrice", stock.AverageBuyPrice);
                    command.Parameters.AddWithValue("@lastKnownPrice", stock.LastKnownPrice.HasValue ? stock.LastKnownPrice.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@lastUpdated", stock.LastUpdated.HasValue ? stock.LastUpdated.Value.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value);

                    long newId = (long)command.ExecuteScalar();
            
                    stock.Id = (int)newId;
                }
            }

            return stock;
        }

        public void Delete(Stock stock)
        {
            using (var connection = GetOpenConnection())
            {
                string sql = "DELETE FROM Stocks WHERE Id = @id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", stock.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}