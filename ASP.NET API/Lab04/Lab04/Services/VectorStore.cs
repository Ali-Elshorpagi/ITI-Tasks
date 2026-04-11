using System.Text.Json;
using Lab04.Models;
using Lab04.Services.Contracts;
using Lab04.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Services
{
    public class VectorStore : IVectorStore
    {
        private readonly ITIContext _context;

        public VectorStore(ITIContext context)
        {
            _context = context;
        }

        public async Task UpsertAsync(IEnumerable<RagVectorRecord> records, CancellationToken cancellationToken)
        {
            foreach (var record in records)
            {
                var chunk = await _context.RagChunks.FirstOrDefaultAsync(c => c.Id == record.ChunkId, cancellationToken);
                if (chunk is null)
                {
                    chunk = new RagChunk
                    {
                        Id = record.ChunkId,
                        RagDocumentId = record.DocumentId,
                        ChunkIndex = record.ChunkIndex,
                        Content = record.Content,
                        ContentHash = record.ContentHash,
                        EmbeddingJson = JsonSerializer.Serialize(record.Vector),
                        CreatedAtUtc = DateTime.UtcNow
                    };
                    _context.RagChunks.Add(chunk);
                }
                else
                {
                    chunk.Content = record.Content;
                    chunk.ContentHash = record.ContentHash;
                    chunk.EmbeddingJson = JsonSerializer.Serialize(record.Vector);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<RagVectorSearchResult>> SearchAsync(float[] queryVector, int topK, Guid? documentId, CancellationToken cancellationToken)
        {
            var query = _context.RagChunks.AsNoTracking().Where(c => !string.IsNullOrWhiteSpace(c.EmbeddingJson));
            if (documentId.HasValue)
            {
                query = query.Where(c => c.RagDocumentId == documentId.Value);
            }

            var allChunks = await query.ToListAsync(cancellationToken);
            var scored = new List<RagVectorSearchResult>(allChunks.Count);

            foreach (var chunk in allChunks)
            {
                var vector = JsonSerializer.Deserialize<float[]>(chunk.EmbeddingJson) ?? Array.Empty<float>();
                if (vector.Length == 0)
                {
                    continue;
                }

                var score = CosineSimilarity(queryVector, vector);
                scored.Add(new RagVectorSearchResult
                {
                    ChunkId = chunk.Id,
                    DocumentId = chunk.RagDocumentId,
                    ChunkIndex = chunk.ChunkIndex,
                    Content = chunk.Content,
                    Score = score
                });
            }

            return scored
                .OrderByDescending(x => x.Score)
                .Take(Math.Max(1, topK))
                .ToList();
        }

        private static double CosineSimilarity(float[] a, float[] b)
        {
            var size = Math.Min(a.Length, b.Length);
            if (size == 0)
            {
                return 0;
            }

            double dot = 0;
            double magA = 0;
            double magB = 0;

            for (var i = 0; i < size; i++)
            {
                dot += a[i] * b[i];
                magA += a[i] * a[i];
                magB += b[i] * b[i];
            }

            if (magA == 0 || magB == 0)
            {
                return 0;
            }

            return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
        }
    }
}
