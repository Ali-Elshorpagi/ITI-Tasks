using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Lab04.Configurations;
using Lab04.Services.Contracts;
using Microsoft.Extensions.Options;

namespace Lab04.Services
{
    public class OpenAiLlmService : ILlmService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAiOptions _options;

        public OpenAiLlmService(HttpClient httpClient, IOptions<OpenAiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GenerateGroundedAnswerAsync(string question, IReadOnlyList<string> contexts, CancellationToken cancellationToken)
        {
            if (contexts.Count == 0)
            {
                return "I don't know based on the indexed documents.";
            }

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                var fallback = string.Join(" ", contexts).Trim();
                return fallback.Length > 500 ? fallback[..500] + "..." : fallback;
            }

            var contextText = string.Join("\n\n", contexts.Select((c, i) => $"[Chunk {i + 1}] {c}"));
            var requestPayload = new
            {
                model = _options.ChatModel,
                messages = new object[]
                {
                    new { role = "system", content = "You are a retrieval QA assistant. Use only the provided context. If not found, say you don't know." },
                    new { role = "user", content = $"Context:\n{contextText}\n\nQuestion: {question}\n\nAnswer with concise text and cite chunk numbers used." }
                },
                temperature = 0.1
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            return json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()
                ?? "I don't know based on the indexed documents.";
        }
    }
}
