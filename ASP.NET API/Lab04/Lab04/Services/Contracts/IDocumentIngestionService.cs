namespace Lab04.Services.Contracts
{
    public interface IDocumentIngestionService
    {
        Task IngestAsync(Guid documentId, CancellationToken cancellationToken);
    }
}
