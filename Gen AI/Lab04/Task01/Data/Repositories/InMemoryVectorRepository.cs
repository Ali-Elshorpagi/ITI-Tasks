using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public sealed class InMemoryVectorRepository : IVectorRepository
{
    private readonly ConcurrentDictionary<Guid, VectorRecord> _vectors = new();

    public Task UpsertAsync(IEnumerable<VectorRecord> vectors, CancellationToken ct)
    {
        foreach (var vector in vectors)
        {
            _vectors[vector.ChunkId] = vector;
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<ChunkMatch>> SearchAsync(float[] queryVector, string queryText, int topK, double minScore, CancellationToken ct)
    {
        var tokens = Tokenize(queryText);
        var matches = _vectors.Values
            .Select(v =>
            {
                var semantic = Cosine(queryVector, v.Vector);
                var lexical = 0d;
                return new ChunkMatch
                {
                    ChunkId = v.ChunkId,
                    SemanticScore = semantic,
                    LexicalScore = lexical,
                    CombinedScore = semantic
                };
            })
            .Where(m => m.CombinedScore >= minScore)
            .OrderByDescending(m => m.CombinedScore)
            .Take(topK)
            .ToList();

        _ = tokens;
        return Task.FromResult((IReadOnlyList<ChunkMatch>)matches);
    }

    public Task<IReadOnlyList<VectorRecord>> GetByDocumentIdAsync(Guid documentId, CancellationToken ct)
    {
        var records = _vectors.Values.Where(v => v.DocumentId == documentId).ToList();
        return Task.FromResult((IReadOnlyList<VectorRecord>)records);
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

    private static HashSet<string> Tokenize(string text)
    {
        return Regex.Matches(text.ToLowerInvariant(), "[a-z0-9]+")
            .Select(x => x.Value)
            .ToHashSet();
    }
}
