using Lab04.Configurations;
using Lab04.DTOs;
using Lab04.Models;
using Lab04.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lab04.Services
{
    public class RagDocumentService : IRagDocumentService
    {
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".txt", ".md"
        };

        private readonly ITIContext _context;
        private readonly IDocumentIngestionQueue _queue;
        private readonly RagOptions _options;

        public RagDocumentService(ITIContext context, IDocumentIngestionQueue queue, IOptions<RagOptions> options)
        {
            _context = context;
            _queue = queue;
            _options = options.Value;
        }

        public async Task<UploadDocumentResponseDto> UploadAndQueueAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Unsupported file extension. Allowed: .pdf, .txt, .md");
            }

            var uploadRoot = Path.IsPathRooted(_options.UploadRoot)
                ? _options.UploadRoot
                : Path.Combine(Directory.GetCurrentDirectory(), _options.UploadRoot);
            Directory.CreateDirectory(uploadRoot);

            var documentId = Guid.NewGuid();
            var storedFileName = $"{documentId}{extension}";
            var storagePath = Path.Combine(uploadRoot, storedFileName);

            await using (var fs = File.Create(storagePath))
            {
                await file.CopyToAsync(fs, cancellationToken);
            }

            var document = new RagDocument
            {
                Id = documentId,
                FileName = file.FileName,
                ContentType = file.ContentType ?? "application/octet-stream",
                StoragePath = storagePath,
                Status = RagDocumentStatus.Uploaded,
                CreatedAtUtc = DateTime.UtcNow
            };

            _context.RagDocuments.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            await _queue.QueueAsync(documentId, cancellationToken);

            return new UploadDocumentResponseDto
            {
                DocumentId = documentId,
                Status = document.Status.ToString()
            };
        }

        public async Task<object?> GetStatusAsync(Guid documentId, CancellationToken cancellationToken)
        {
            var document = await _context.RagDocuments.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken);
            if (document is null)
            {
                return null;
            }

            var chunkCount = await _context.RagChunks.CountAsync(c => c.RagDocumentId == documentId, cancellationToken);
            return new
            {
                document.Id,
                document.FileName,
                Status = document.Status.ToString(),
                document.ErrorMessage,
                document.CreatedAtUtc,
                document.ProcessedAtUtc,
                ChunkCount = chunkCount
            };
        }
    }
}
