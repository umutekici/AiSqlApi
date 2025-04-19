namespace AiSqlApi.Core.Services
{
    using AiSqlApi.Core.Interfaces;
    using AiSqlApi.Models;
    using System.Text.Json;

    public class SchemaService : ISchemaService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _schemaFilePath;

        public SchemaService(IWebHostEnvironment env)
        {
            _env = env;
            _schemaFilePath = Path.Combine(_env.ContentRootPath, "Schemas", "tableschema.json");
        }

        public List<TableSchema> GetTableSchemas()
        {
            if (!File.Exists(_schemaFilePath))
            {
                throw new FileNotFoundException("Schema file not found: " + _schemaFilePath);
            }

            var json = File.ReadAllText(_schemaFilePath);
            var tables = JsonSerializer.Deserialize<List<TableSchema>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tables ?? new List<TableSchema>();
        }
    }

}
