using Task01.Data.Entities;

namespace Task01.Data.Repositories;

public interface IChunkRepository
{
    Task AddRangeAsync(IEnumerable<ChunkEntity> chunks, CancellationToken ct);
    Task<IReadOnlyList<ChunkEntity>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct);
}
