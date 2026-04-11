using Lab04.DTOs;
using Microsoft.AspNetCore.Http;

namespace Lab04.Services.Contracts
{
    public interface IRagDocumentService
    {
        Task<UploadDocumentResponseDto> UploadAndQueueAsync(IFormFile file, CancellationToken cancellationToken);
        Task<object?> GetStatusAsync(Guid documentId, CancellationToken cancellationToken);
    }
}
