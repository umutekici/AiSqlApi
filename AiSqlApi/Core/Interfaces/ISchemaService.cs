using AiSqlApi.Models;

namespace AiSqlApi.Core.Interfaces
{
    public interface ISchemaService
    {
        List<TableSchema> GetTableSchemas();
    }
}
