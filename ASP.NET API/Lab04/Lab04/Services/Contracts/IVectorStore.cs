using Lab04.Services.Models;

namespace Lab04.Services.Contracts
{
    public interface IVectorStore
    {
        Task UpsertAsync(IEnumerable<RagVectorRecord> records, CancellationToken cancellationToken);
        Task<IReadOnlyList<RagVectorSearchResult>> SearchAsync(float[] queryVector, int topK, Guid? documentId, CancellationToken cancellationToken);
    }
}
