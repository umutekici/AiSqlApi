using AiSqlApi.Core.Interfaces;
using AiSqlApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiSqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AiSqlController : ControllerBase
    {
        private readonly IAiService _aiService;
        private readonly IDatabaseService _dbService;
        private readonly ISchemaService _schemaService;

        public AiSqlController(IAiService aiService, IDatabaseService dbService, ISchemaService schemaService)
        {
            _aiService = aiService;
            _dbService = dbService;
            _schemaService = schemaService;
        }

        [HttpPost("run")]
        public async Task<string> Run([FromBody] Prompt input)
        {
            var tables = _schemaService.GetTableSchemas();

            string tableInfo = string.Join("\n\n", tables.Select(t =>
                $"{t.TableName} ({string.Join(", ", t.Columns.Select(c => $"{c.Name} {c.DataType}"))})"
            ));

            if (string.IsNullOrWhiteSpace(tableInfo))
            {
                return "Table not found.";
            }

            string prompt = $"""
                    Generate only a SELECT query for SQL Server based on the following table structure.
                    {tableInfo}

                    Question: {input.Text}
                    SQL:
                    """;

            // Get SQL query from AI service
            string gptSql = await _aiService.GetAnswerFromGroq(prompt);

            try
            {
                if (string.IsNullOrWhiteSpace(gptSql) || !gptSql.ToUpper().StartsWith("SELECT"))
                {
                    throw new Exception("Invalid SQL query received.");
                }

                // Execute SQL query using DB service
                string result = await _dbService.ExecuteSqlAsync(gptSql);
                return $"SQL Query: {gptSql}\nResult: {result}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}\nGPT SQL: {gptSql}";
            }
        }
    }

}
