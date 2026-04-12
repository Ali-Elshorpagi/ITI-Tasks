using Task01.Models;

namespace Task01.Services.Ingestion;

public interface IIngestionService
{
    Task<UploadResult> EnqueueUploadAsync(IFormFile file, CancellationToken ct);
    Task ProcessDocumentAsync(Guid documentId, CancellationToken ct);
}
