namespace AiSqlApi.Models
{
    public class ColumnDefinition
    {
        public string Name { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
