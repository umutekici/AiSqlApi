namespace AiSqlApi.Models
{
    public class TableSchema
    {
        public string TableName { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
        public List<ColumnDefinition> Columns { get; set; } = new();
    }

}
