using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public interface IVectorRepository
{
    Task UpsertAsync(IEnumerable<VectorRecord> vectors, CancellationToken ct);
    Task<IReadOnlyList<ChunkMatch>> SearchAsync(float[] queryVector, string queryText, int topK, double minScore, CancellationToken ct);
    Task<IReadOnlyList<VectorRecord>> GetByDocumentIdAsync(Guid documentId, CancellationToken ct);
}
