using Lab04.Services.Contracts;
using Lab04.Services.Models;

namespace Lab04.Services
{
    public class SimpleRagReranker : IRagReranker
    {
        public IReadOnlyList<RagVectorSearchResult> Rerank(string question, IReadOnlyList<RagVectorSearchResult> candidates)
        {
            var queryTerms = question.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(t => t.ToLowerInvariant())
                .ToHashSet();

            return candidates
                .Select(c =>
                {
                    var textTerms = c.Content.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(t => t.ToLowerInvariant())
                        .ToHashSet();
                    var overlap = queryTerms.Count == 0 ? 0 : queryTerms.Count(q => textTerms.Contains(q));
                    var lexicalBoost = queryTerms.Count == 0 ? 0.0 : (double)overlap / queryTerms.Count * 0.05;

                    return new RagVectorSearchResult
                    {
                        ChunkId = c.ChunkId,
                        DocumentId = c.DocumentId,
                        ChunkIndex = c.ChunkIndex,
                        Content = c.Content,
                        Score = c.Score + lexicalBoost
                    };
                })
                .OrderByDescending(c => c.Score)
                .ToList();
        }
    }
}
