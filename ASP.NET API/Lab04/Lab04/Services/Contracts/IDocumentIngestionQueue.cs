namespace Lab04.Services.Contracts
{
    public interface IDocumentIngestionQueue
    {
        ValueTask QueueAsync(Guid documentId, CancellationToken cancellationToken);
        ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken);
    }
}
