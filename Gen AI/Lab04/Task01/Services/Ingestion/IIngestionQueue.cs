namespace Task01.Services.Ingestion;

public interface IIngestionQueue
{
    ValueTask EnqueueAsync(Guid documentId, CancellationToken ct);
    ValueTask<Guid> DequeueAsync(CancellationToken ct);
}
