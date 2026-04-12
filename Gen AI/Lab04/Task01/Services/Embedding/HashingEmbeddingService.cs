using System.Security.Cryptography;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using Task01.Models;

namespace Task01.Services.Embedding;

public sealed class HashingEmbeddingService : IEmbeddingService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenAiOptions _openAiOptions;
    private readonly ILogger<HashingEmbeddingService> _logger;
    private readonly int _dimensions;

    public HashingEmbeddingService(
        IHttpClientFactory httpClientFactory,
        IOptions<RagOptions> ragOptions,
        IOptions<OpenAiOptions> openAiOptions,
        ILogger<HashingEmbeddingService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _openAiOptions = openAiOptions.Value;
        _logger = logger;
        _dimensions = ragOptions.Value.EmbeddingDimensions;
    }

    public async Task<float[]> EmbedAsync(string text, CancellationToken ct)
    {
        var vectors = await EmbedBatchAsync([text], ct);
        return vectors[0];
    }

    public async Task<IReadOnlyList<float[]>> EmbedBatchAsync(IReadOnlyList<string> texts, CancellationToken ct)
    {
        if (texts.Count == 0)
        {
            return [];
        }

        if (string.IsNullOrWhiteSpace(_openAiOptions.ApiKey))
        {
            return texts.Select(CreateVector).ToList();
        }

        try
        {
            return await GenerateOpenAiEmbeddingsAsync(texts, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Falling back to local hashing embeddings because the OpenAI embedding call failed.");
            return texts.Select(CreateVector).ToList();
        }
    }

    private async Task<IReadOnlyList<float[]>> GenerateOpenAiEmbeddingsAsync(IReadOnlyList<string> texts, CancellationToken ct)
    {
        var client = CreateClient();
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAITextEmbeddingGeneration(_openAiOptions.EmbeddingModel, _openAiOptions.ApiKey, httpClient: client);
        var kernel = builder.Build();

        var embeddingService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
        var embeddings = await embeddingService.GenerateEmbeddingsAsync(texts.ToList(), cancellationToken: ct);

        return embeddings.Select(e => e.ToArray()).ToList();
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient(nameof(HashingEmbeddingService));
        client.BaseAddress = new Uri(_openAiOptions.BaseUrl.TrimEnd('/') + "/");
        // Semantic Kernel will append appropriate headers internally, but setting BaseAddress helps point to standard or custom endpoints.
        return client;
    }

    private float[] CreateVector(string text)
    {
        var vector = new float[_dimensions];
        var tokens = Regex.Matches(text.ToLowerInvariant(), "[a-z0-9]+")
            .Select(m => m.Value)
            .ToList();

        foreach (var token in tokens)
        {
            var index = HashToIndex(token, _dimensions);
            vector[index] += 1f;
        }

        Normalize(vector);
        return vector;
    }

    private static int HashToIndex(string token, int dimensions)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        var raw = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
        return raw % dimensions;
    }

    private static void Normalize(float[] vector)
    {
        var norm = 0d;
        for (var i = 0; i < vector.Length; i++)
        {
            norm += vector[i] * vector[i];
        }

        norm = Math.Sqrt(norm);
        if (norm == 0)
        {
            return;
        }

        for (var i = 0; i < vector.Length; i++)
        {
            vector[i] = (float)(vector[i] / norm);
        }
    }
}




