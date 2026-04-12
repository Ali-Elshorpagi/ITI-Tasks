using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Task01.Data.Entities;
using Task01.Models;

namespace Task01.Data.Repositories;

public sealed class ChromaVectorRepository : IVectorRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ChromaVectorRepository> _logger;
    private readonly VectorStoreOptions _options;
    private readonly SemaphoreSlim _collectionLock = new(1, 1);
    private readonly ConcurrentDictionary<Guid, VectorRecord> _fallbackVectors = new();

    private string? _collectionId;
    private bool _fallbackMode;

    public ChromaVectorRepository(
        IHttpClientFactory httpClientFactory,
        IOptions<VectorStoreOptions> options,
        ILogger<ChromaVectorRepository> logger)
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

        if (_fallbackMode)
        {
            UpsertInMemory(points);
            return;
        }

        try
        {
            var collectionId = await EnsureCollectionAsync(ct);

            var request = new
            {
                ids = points.Select(x => x.ChunkId.ToString()).ToArray(),
                embeddings = points.Select(x => x.Vector).ToArray(),
                metadatas = points.Select(x => new Dictionary<string, object>
                {
                    ["chunkId"] = x.ChunkId.ToString(),
                    ["documentId"] = x.DocumentId.ToString()
                }).ToArray(),
                documents = points.Select(_ => string.Empty).ToArray()
            };

            var client = CreateClient();
            using var response = await client.PostAsJsonAsync($"api/v1/collections/{collectionId}/upsert", request, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Chroma upsert failed: {response.StatusCode} - {body}");
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
            var collectionId = await EnsureCollectionAsync(ct);

            var request = new
            {
                query_embeddings = new[] { queryVector },
                n_results = topK,
                include = new[] { "distances", "metadatas" }
            };

            var client = CreateClient();
            using var response = await client.PostAsJsonAsync($"api/v1/collections/{collectionId}/query", request, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Chroma query failed: {response.StatusCode} - {body}");
            }

            using var doc = JsonDocument.Parse(body);
            if (!doc.RootElement.TryGetProperty("ids", out var idsNode)
                || idsNode.ValueKind != JsonValueKind.Array
                || idsNode.GetArrayLength() == 0)
            {
                return [];
            }

            var idRow = idsNode[0];
            JsonElement distanceRow = default;
            var hasDistances = doc.RootElement.TryGetProperty("distances", out var distancesNode)
                && distancesNode.ValueKind == JsonValueKind.Array
                && distancesNode.GetArrayLength() > 0;

            if (hasDistances)
            {
                distanceRow = distancesNode[0];
            }

            var matches = new List<ChunkMatch>();
            for (var i = 0; i < idRow.GetArrayLength(); i++)
            {
                var idText = idRow[i].GetString();
                if (!Guid.TryParse(idText, out var chunkId))
                {
                    continue;
                }

                var distance = hasDistances && i < distanceRow.GetArrayLength()
                    ? distanceRow[i].GetDouble()
                    : 1d;

                var score = 1d - distance;
                if (score < minScore)
                {
                    continue;
                }

                matches.Add(new ChunkMatch
                {
                    ChunkId = chunkId,
                    SemanticScore = score,
                    LexicalScore = 0,
                    CombinedScore = score
                });
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

    private async Task<string> EnsureCollectionAsync(CancellationToken ct)
    {
        if (!string.IsNullOrWhiteSpace(_collectionId))
        {
            return _collectionId;
        }

        await _collectionLock.WaitAsync(ct);
        try
        {
            if (!string.IsNullOrWhiteSpace(_collectionId))
            {
                return _collectionId;
            }

            var client = CreateClient();
            using var listResponse = await client.GetAsync("api/v1/collections", ct);
            var listBody = await listResponse.Content.ReadAsStringAsync(ct);

            if (!listResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Chroma list collections failed: {listResponse.StatusCode} - {listBody}");
            }

            using (var listDoc = JsonDocument.Parse(listBody))
            {
                foreach (var item in listDoc.RootElement.EnumerateArray())
                {
                    var name = item.TryGetProperty("name", out var nameNode) ? nameNode.GetString() : null;
                    if (!string.Equals(name, _options.ChromaCollection, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    _collectionId = item.GetProperty("id").GetString();
                    if (!string.IsNullOrWhiteSpace(_collectionId))
                    {
                        return _collectionId;
                    }
                }
            }

            var createBody = new
            {
                name = _options.ChromaCollection,
                metadata = new Dictionary<string, object>
                {
                    ["hnsw:space"] = "cosine"
                }
            };

            using var createResponse = await client.PostAsJsonAsync("api/v1/collections", createBody, ct);
            var createText = await createResponse.Content.ReadAsStringAsync(ct);
            if (!createResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Chroma create collection failed: {createResponse.StatusCode} - {createText}");
            }

            using var createDoc = JsonDocument.Parse(createText);
            _collectionId = createDoc.RootElement.GetProperty("id").GetString();

            if (string.IsNullOrWhiteSpace(_collectionId))
            {
                throw new InvalidOperationException("Chroma returned empty collection id.");
            }

            _logger.LogInformation("Chroma collection '{CollectionName}' is ready with id '{CollectionId}'.", _options.ChromaCollection, _collectionId);
            return _collectionId;
        }
        finally
        {
            _collectionLock.Release();
        }
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient(nameof(ChromaVectorRepository));
        client.BaseAddress = new Uri(_options.ChromaUrl.TrimEnd('/') + "/");
        return client;
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
        _logger.LogWarning(ex, "Chroma is unavailable. Switching to in-memory vector fallback.");
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
}
