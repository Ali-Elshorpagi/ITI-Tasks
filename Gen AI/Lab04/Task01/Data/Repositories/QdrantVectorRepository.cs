using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Task01.Data.Entities;
using Task01.Models;

namespace Task01.Data.Repositories;

public sealed class QdrantVectorRepository : IVectorRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<QdrantVectorRepository> _logger;
    private readonly VectorStoreOptions _options;
    private readonly SemaphoreSlim _collectionLock = new(1, 1);
    private readonly ConcurrentDictionary<Guid, VectorRecord> _fallbackVectors = new();
    private int _collectionVectorSize;
    private bool _fallbackMode;

    public QdrantVectorRepository(
        IHttpClientFactory httpClientFactory,
        IOptions<VectorStoreOptions> options,
        ILogger<QdrantVectorRepository> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _options = options.Value;
    }

    public async Task UpsertAsync(IEnumerable<VectorRecord> vectors, CancellationToken ct)
    {
        var points = vectors.ToList();
        if (points.Count == 0)
        {
            return;
        }

        try
        {
            await EnsureCollectionAsync(points[0].Vector.Length, ct);

            var request = new
            {
                points = points.Select(p => new
                {
                    id = p.ChunkId,
                    vector = p.Vector,
                    payload = new
                    {
                        chunkId = p.ChunkId,
                        documentId = p.DocumentId
                    }
                })
            };

            var client = CreateClient();
            using var response = await client.PutAsJsonAsync($"collections/{_options.QdrantCollection}/points?wait=true", request, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Qdrant upsert failed: {response.StatusCode} - {body}");
            }
        }
        catch (Exception ex)
        {
            EnableFallback(ex);
            UpsertInMemory(points);
        }
    }

    public async Task<IReadOnlyList<ChunkMatch>> SearchAsync(float[] queryVector, string queryText, int topK, double minScore, CancellationToken ct)
    {
        if (_fallbackMode)
        {
            return SearchInMemory(queryVector, topK, minScore);
        }

        try
        {
            await EnsureCollectionAsync(queryVector.Length, ct);

            var request = new
            {
                vector = queryVector,
                limit = topK,
                with_payload = true,
                score_threshold = minScore
            };

            var client = CreateClient();
            using var response = await client.PostAsJsonAsync($"collections/{_options.QdrantCollection}/points/search", request, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Qdrant search failed: {response.StatusCode} - {body}");
            }

            using var doc = JsonDocument.Parse(body);
            if (!doc.RootElement.TryGetProperty("result", out var result) || result.ValueKind != JsonValueKind.Array)
            {
                return [];
            }

            var matches = new List<ChunkMatch>();
            foreach (var item in result.EnumerateArray())
            {
                Guid chunkId;
                if (item.TryGetProperty("payload", out var payload)
                    && payload.TryGetProperty("chunkId", out var chunkIdElement)
                    && Guid.TryParse(chunkIdElement.GetString(), out chunkId))
                {
                    var score = item.TryGetProperty("score", out var scoreElement) ? scoreElement.GetDouble() : 0;
                    matches.Add(new ChunkMatch
                    {
                        ChunkId = chunkId,
                        SemanticScore = score,
                        LexicalScore = 0,
                        CombinedScore = score
                    });
                }
            }

            return matches;
        }
        catch (Exception ex)
        {
            EnableFallback(ex);
            return SearchInMemory(queryVector, topK, minScore);
        }
    }

    public Task<IReadOnlyList<VectorRecord>> GetByDocumentIdAsync(Guid documentId, CancellationToken ct)
    {
        var records = _fallbackVectors.Values.Where(v => v.DocumentId == documentId).ToList();
        return Task.FromResult((IReadOnlyList<VectorRecord>)records);
    }

    private void UpsertInMemory(IEnumerable<VectorRecord> vectors)
    {
        foreach (var vector in vectors)
        {
            _fallbackVectors[vector.ChunkId] = vector;
        }
    }

    private IReadOnlyList<ChunkMatch> SearchInMemory(float[] queryVector, int topK, double minScore)
    {
        return _fallbackVectors.Values
            .Select(v =>
            {
                var score = Cosine(queryVector, v.Vector);
                return new ChunkMatch
                {
                    ChunkId = v.ChunkId,
                    SemanticScore = score,
                    LexicalScore = 0,
                    CombinedScore = score
                };
            })
            .Where(x => x.CombinedScore >= minScore)
            .OrderByDescending(x => x.CombinedScore)
            .Take(topK)
            .ToList();
    }

    private void EnableFallback(Exception ex)
    {
        if (_fallbackMode)
        {
            return;
        }

        _fallbackMode = true;
        _logger.LogWarning(ex, "Qdrant is unavailable. Switching to in-memory vector fallback.");
    }

    private static double Cosine(float[] a, float[] b)
    {
        if (a.Length != b.Length || a.Length == 0)
        {
            return 0d;
        }

        double dot = 0;
        double normA = 0;
        double normB = 0;

        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        if (normA <= 0 || normB <= 0)
        {
            return 0d;
        }

        return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
    }

    private async Task EnsureCollectionAsync(int vectorSize, CancellationToken ct)
    {
        if (_collectionVectorSize == vectorSize)
        {
            return;
        }

        await _collectionLock.WaitAsync(ct);
        try
        {
            if (_collectionVectorSize == vectorSize)
            {
                return;
            }

            var request = new
            {
                vectors = new
                {
                    size = vectorSize,
                    distance = "Cosine"
                }
            };

            var client = CreateClient();
            using var response = await client.PutAsJsonAsync($"collections/{_options.QdrantCollection}", request, ct);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                throw new InvalidOperationException($"Qdrant collection init failed: {response.StatusCode} - {body}");
            }

            _collectionVectorSize = vectorSize;
            _logger.LogInformation("Qdrant collection '{Collection}' is ready with vector size {VectorSize}.", _options.QdrantCollection, vectorSize);
        }
        finally
        {
            _collectionLock.Release();
        }
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient(nameof(QdrantVectorRepository));
        client.BaseAddress = new Uri(_options.QdrantUrl.TrimEnd('/') + "/");

        if (!string.IsNullOrWhiteSpace(_options.QdrantApiKey))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.QdrantApiKey);
            client.DefaultRequestHeaders.Remove("api-key");
            client.DefaultRequestHeaders.Add("api-key", _options.QdrantApiKey);
        }

        return client;
    }
}
