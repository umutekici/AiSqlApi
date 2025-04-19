namespace AiSqlApi.Core.Interfaces
{
    public interface IDatabaseService
    {
       Task<string> ExecuteSqlAsync(string sql);
    }
}
