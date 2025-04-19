using AiSqlApi.Core.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text;

namespace AiSqlApi.Core.Services
{
    public class AiService : IAiService
    {
        private readonly IConfiguration _config;

        public AiService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> GetAnswerFromGroq(string prompt)
        {
            try
            {
                var answer = await AskGroq(prompt);
                return answer;
            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message;
            }
        }

        public async Task<string> AskGroq(string prompt)
        {
            var apiKey = _config["OpenAI:ApiKey"];
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = _config["OpenAI:Model"],
                messages = new[] {
                new { role = "user", content = prompt } },
                temperature = 0.7 // Controls the creativity level of the AI's response
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_config["OpenAI:ApiUrl"], content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(jsonResponse);

                var rawContent = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                var match = Regex.Match(rawContent, "```sql\\s*(.*?)\\s*```", RegexOptions.Singleline);
                return match.Success ? match.Groups[1].Value.Trim() : rawContent?.Trim() ?? throw new Exception("Response is empty.");
            }
            else
            {
                throw new Exception($"API Error: {jsonResponse}");
            }
        }
    }

}
