using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public interface IDocumentRepository
{
    Task AddAsync(DocumentEntity document, CancellationToken ct);
    Task<DocumentEntity?> GetAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(DocumentEntity document, CancellationToken ct);
}
