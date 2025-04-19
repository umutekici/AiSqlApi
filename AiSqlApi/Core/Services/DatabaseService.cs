using AiSqlApi.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace AiSqlApi.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _config;

        public DatabaseService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> ExecuteSqlAsync(string sql)
        {
            var connStr = _config.GetConnectionString("DefaultConnection");
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            var cmd = new SqlCommand(sql, conn);
            var reader = await cmd.ExecuteReaderAsync();
            var resultList = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader[i];
                }
                resultList.Add(row);
            }

            return JsonSerializer.Serialize(resultList);
        }
    }

}
