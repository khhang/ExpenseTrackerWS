using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace expense_tracker.Repositories
{
    public interface IDatabaseAccessor
    {
        Task<IEnumerable<T>> QueryMultipleProcedureAsync<T>(string spName, dynamic parameters = null) where T : class;
        Task<T> QueryProcedureAsync<T>(string spName, dynamic parameters = null) where T : class;
        Task ExecuteProcedureAsync(string spName, dynamic parameters = null);
    }

    public class DatabaseAccessor : IDatabaseAccessor
    {
        private readonly string _connectionString;
        public DatabaseAccessor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> QueryMultipleProcedureAsync<T>(string spName, dynamic parameters = null) where T : class
        {
            var results = new List<T>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using var command = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
                if (parameters != null)
                {
                    var props = ((object)parameters)
                        .GetType()
                        .GetProperties();

                    foreach (var prop in props)
                    {
                        command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
                    }
                }

                conn.Open();

                using var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        var convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        var sqlValue = reader[property.Name];
                        property.SetValue(item,
                            DBNull.Value.Equals(sqlValue) ? null : Convert.ChangeType(sqlValue, convertTo));
                    }
                    results.Add(item);
                }
            }
            return results;
        }

        public async Task<T> QueryProcedureAsync<T>(string spName, dynamic parameters = null) where T : class
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using var command = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
                if (parameters != null)
                {
                    var props = ((object)parameters)
                        .GetType()
                        .GetProperties();

                    foreach (var prop in props)
                    {
                        command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
                    }
                }

                conn.Open();

                using var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        var convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                    return item;
                }
            }

            return null;
        }

        public async Task ExecuteProcedureAsync(string spName, dynamic parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using var command = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };
                if (parameters != null)
                {
                    var props = ((object)parameters)
                        .GetType()
                        .GetProperties();

                    foreach (var prop in props)
                    {
                        command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
                    }
                }

                conn.Open();

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
