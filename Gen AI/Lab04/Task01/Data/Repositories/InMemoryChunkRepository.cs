using System.Collections.Concurrent;
using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public sealed class InMemoryChunkRepository : IChunkRepository
{
    private readonly ConcurrentDictionary<Guid, ChunkEntity> _chunks = new();

    public Task AddRangeAsync(IEnumerable<ChunkEntity> chunks, CancellationToken ct)
    {
        foreach (var chunk in chunks)
        {
            _chunks[chunk.Id] = chunk;
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<ChunkEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct)
    {
        var idSet = ids.ToHashSet();
        var list = _chunks.Values.Where(c => idSet.Contains(c.Id)).ToList();
        return Task.FromResult((IReadOnlyList<ChunkEntity>)list);
    }
}
