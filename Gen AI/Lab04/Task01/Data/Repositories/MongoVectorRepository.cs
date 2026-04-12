using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Task01.Data.Entities;
using Task01.Models;

namespace Task01.Data.Repositories;

public sealed class MongoVectorRepository : IVectorRepository
{
    private readonly IMongoCollection<VectorRecord> _collection;
    private readonly ILogger<MongoVectorRepository> _logger;
    private readonly ConcurrentDictionary<Guid, VectorRecord> _cachedVectors = new();
    private readonly ConcurrentDictionary<Guid, VectorRecord> _fallbackVectors = new();
    private readonly SemaphoreSlim _cacheLock = new(1, 1);
    private bool _cacheLoaded;
    private bool _fallbackMode;

    public MongoVectorRepository(IOptions<MongoDbOptions> options, ILogger<MongoVectorRepository> logger)
    {
        _logger = logger;
        var mongoOptions = options.Value;
        var client = new MongoClient(mongoOptions.ConnectionString);
        var database = client.GetDatabase(mongoOptions.Database);
        _collection = database.GetCollection<VectorRecord>(mongoOptions.VectorCollection);
    }

    public async Task UpsertAsync(IEnumerable<VectorRecord> vectors, CancellationToken ct)
    {
        var items = vectors.ToList();
        if (items.Count == 0)
        {
            return;
        }

        foreach (var item in items)
        {
            _cachedVectors[item.ChunkId] = item;
        }

        if (_fallbackMode)
        {
            UpsertInMemory(items);
            return;
        }

        try
        {
            foreach (var vector in items)
            {
                var filter = Builders<VectorRecord>.Filter.Eq(x => x.ChunkId, vector.ChunkId);
                await _collection.ReplaceOneAsync(filter, vector, new ReplaceOptions { IsUpsert = true }, ct);
            }
        }
        catch (Exception ex)
        {
            EnableFallback(ex);
            UpsertInMemory(items);
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
            await EnsureCacheLoadedAsync(ct);
            return _cachedVectors.Values
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
        catch (Exception ex)
        {
            EnableFallback(ex);
            return SearchInMemory(queryVector, topK, minScore);
        }
    }

    public async Task<IReadOnlyList<VectorRecord>> GetByDocumentIdAsync(Guid documentId, CancellationToken ct)
    {
        if (_fallbackMode)
        {
            return _fallbackVectors.Values.Where(v => v.DocumentId == documentId).ToList();
        }

        try
        {
            await EnsureCacheLoadedAsync(ct);
            return _cachedVectors.Values.Where(v => v.DocumentId == documentId).ToList();
        }
        catch (Exception ex)
        {
            EnableFallback(ex);
            return _fallbackVectors.Values.Where(v => v.DocumentId == documentId).ToList();
        }
    }

    private async Task EnsureCacheLoadedAsync(CancellationToken ct)
    {
        if (_cacheLoaded)
        {
            return;
        }

        await _cacheLock.WaitAsync(ct);
        try
        {
            if (_cacheLoaded)
            {
                return;
            }

            var vectors = await _collection.Find(_ => true).ToListAsync(ct);
            foreach (var vector in vectors)
            {
                _cachedVectors[vector.ChunkId] = vector;
            }

            _cacheLoaded = true;
        }
        finally
        {
            _cacheLock.Release();
        }
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
        _logger.LogWarning(ex, "Mongo vector store is unavailable. Switching to in-memory vector fallback.");
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
