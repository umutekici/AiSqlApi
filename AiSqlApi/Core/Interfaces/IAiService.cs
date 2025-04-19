namespace AiSqlApi.Core.Interfaces
{
    public interface IAiService
    {
        Task<string> GetAnswerFromGroq(string prompt);
        Task<string> AskGroq(string prompt);
    }
}
