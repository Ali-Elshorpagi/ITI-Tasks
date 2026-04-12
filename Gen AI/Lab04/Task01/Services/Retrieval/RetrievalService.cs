using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Task01.Data.Repositories;
using Task01.Models;
using Task01.Services.Embedding;

namespace Task01.Services.Retrieval;

public sealed class RetrievalService : IRetrievalService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly IVectorRepository _vectorRepository;
    private readonly IChunkRepository _chunkRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly RagOptions _options;

    public RetrievalService(
        IEmbeddingService embeddingService,
        IVectorRepository vectorRepository,
        IChunkRepository chunkRepository,
        IDocumentRepository documentRepository,
        IOptions<RagOptions> options)
    {
        _embeddingService = embeddingService;
        _vectorRepository = vectorRepository;
        _chunkRepository = chunkRepository;
        _documentRepository = documentRepository;
        _options = options.Value;
    }

    public async Task<IReadOnlyList<RetrievedChunk>> RetrieveAsync(string question, int topK, CancellationToken ct)
    {
        topK = Math.Clamp(topK, 1, _options.MaxRetrievedChunks);

        var queryVector = await _embeddingService.EmbedAsync(question, ct);
        var semanticCandidates = await _vectorRepository.SearchAsync(
            queryVector,
            question,
            Math.Max(topK * 4, 20),
            _options.MinScore,
            ct);

        var chunks = await _chunkRepository.GetByIdsAsync(semanticCandidates.Select(m => m.ChunkId), ct);
        var chunkMap = chunks.ToDictionary(c => c.Id);
        var queryTokens = Tokenize(question);
        var documentCache = new Dictionary<Guid, Data.Entities.DocumentEntity?>();

        var ranked = new List<RetrievedChunk>();

        foreach (var candidate in semanticCandidates)
        {
            if (!chunkMap.TryGetValue(candidate.ChunkId, out var chunk))
            {
                continue;
            }

            var lexical = ComputeLexicalScore(queryTokens, Tokenize(chunk.Content));
            var combined = (candidate.SemanticScore * 0.75) + (lexical * 0.25);

            if (!documentCache.TryGetValue(chunk.DocumentId, out var document))
            {
                document = await _documentRepository.GetAsync(chunk.DocumentId, ct);
                documentCache[chunk.DocumentId] = document;
            }

            if (document is null)
            {
                continue;
            }

            ranked.Add(new RetrievedChunk
            {
                DocumentId = chunk.DocumentId,
                FileName = document.FileName,
                ChunkId = chunk.Id,
                ChunkIndex = chunk.ChunkIndex,
                Content = chunk.Content,
                Score = combined
            });
        }

        return ranked
            .OrderByDescending(x => x.Score)
            .Take(topK)
            .ToList();
    }

    private static HashSet<string> Tokenize(string input)
    {
        return Regex.Matches(input.ToLowerInvariant(), "[a-z0-9]+")
            .Select(m => m.Value)
            .ToHashSet();
    }

    private static double ComputeLexicalScore(HashSet<string> queryTokens, HashSet<string> chunkTokens)
    {
        if (queryTokens.Count == 0 || chunkTokens.Count == 0)
        {
            return 0;
        }

        var intersection = queryTokens.Intersect(chunkTokens).Count();
        var union = queryTokens.Union(chunkTokens).Count();
        return union == 0 ? 0 : (double)intersection / union;
    }
}
