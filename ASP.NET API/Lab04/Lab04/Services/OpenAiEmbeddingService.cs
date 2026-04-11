using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Lab04.Configurations;
using Lab04.Services.Contracts;
using Microsoft.Extensions.Options;

namespace Lab04.Services
{
    public class OpenAiEmbeddingService : IEmbeddingService
    {
        private const int FallbackDimensions = 256;
        private readonly HttpClient _httpClient;
        private readonly OpenAiOptions _options;

        public OpenAiEmbeddingService(HttpClient httpClient, IOptions<OpenAiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                return BuildFallbackEmbedding(text);
            }

            var requestObject = new
            {
                model = _options.EmbeddingModel,
                input = text
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/embeddings")
            {
                Content = new StringContent(JsonSerializer.Serialize(requestObject), Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
            var embeddingElements = json.RootElement.GetProperty("data")[0].GetProperty("embedding");
            var embedding = new float[embeddingElements.GetArrayLength()];
            var index = 0;
            foreach (var number in embeddingElements.EnumerateArray())
            {
                embedding[index++] = number.GetSingle();
            }

            return embedding;
        }

        private static float[] BuildFallbackEmbedding(string text)
        {
            var vector = new float[FallbackDimensions];
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (words.Length == 0)
            {
                return vector;
            }

            foreach (var word in words)
            {
                var hash = SHA256.HashData(Encoding.UTF8.GetBytes(word.ToLowerInvariant()));
                var bucket = BitConverter.ToUInt32(hash, 0) % (uint)FallbackDimensions;
                vector[bucket] += 1f;
            }

            var magnitude = Math.Sqrt(vector.Sum(v => v * v));
            if (magnitude == 0)
            {
                return vector;
            }

            for (var i = 0; i < vector.Length; i++)
            {
                vector[i] = (float)(vector[i] / magnitude);
            }

            return vector;
        }
    }
}
