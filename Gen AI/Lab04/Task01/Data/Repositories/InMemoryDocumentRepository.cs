using System.Collections.Concurrent;
using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public sealed class InMemoryDocumentRepository : IDocumentRepository
{
    private readonly ConcurrentDictionary<Guid, DocumentEntity> _documents = new();

    public Task AddAsync(DocumentEntity document, CancellationToken ct)
    {
        _documents[document.Id] = document;
        return Task.CompletedTask;
    }

    public Task<DocumentEntity?> GetAsync(Guid id, CancellationToken ct)
    {
        _documents.TryGetValue(id, out var document);
        return Task.FromResult(document);
    }

    public Task UpdateAsync(DocumentEntity document, CancellationToken ct)
    {
        document.UpdatedUtc = DateTime.UtcNow;
        _documents[document.Id] = document;
        return Task.CompletedTask;
    }
}
