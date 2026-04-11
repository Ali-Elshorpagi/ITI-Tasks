using System.Security.Cryptography;
using System.Text;
using Lab04.Models;
using Lab04.Services.Contracts;
using Lab04.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Services
{
    public class DocumentIngestionService : IDocumentIngestionService
    {
        private readonly ITIContext _context;
        private readonly ITextExtractorResolver _extractorResolver;
        private readonly ITextPreprocessor _preprocessor;
        private readonly IChunkingService _chunkingService;
        private readonly IEmbeddingService _embeddingService;
        private readonly IVectorStore _vectorStore;

        public DocumentIngestionService(
            ITIContext context,
            ITextExtractorResolver extractorResolver,
            ITextPreprocessor preprocessor,
            IChunkingService chunkingService,
            IEmbeddingService embeddingService,
            IVectorStore vectorStore)
        {
            _context = context;
            _extractorResolver = extractorResolver;
            _preprocessor = preprocessor;
            _chunkingService = chunkingService;
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
        }

        public async Task IngestAsync(Guid documentId, CancellationToken cancellationToken)
        {
            var document = await _context.RagDocuments.FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken)
                ?? throw new InvalidOperationException($"Document {documentId} was not found.");

            try
            {
                document.Status = RagDocumentStatus.Processing;
                document.ErrorMessage = null;
                await _context.SaveChangesAsync(cancellationToken);

                var extension = Path.GetExtension(document.StoragePath);
                var extractor = _extractorResolver.Resolve(extension);
                var rawText = await extractor.ExtractAsync(document.StoragePath, cancellationToken);
                var normalizedText = _preprocessor.Normalize(rawText);
                var chunks = _chunkingService.Chunk(normalizedText);

                var existingChunks = _context.RagChunks.Where(c => c.RagDocumentId == documentId);
                _context.RagChunks.RemoveRange(existingChunks);
                await _context.SaveChangesAsync(cancellationToken);

                var vectorRecords = new List<RagVectorRecord>();
                for (var i = 0; i < chunks.Count; i++)
                {
                    var content = chunks[i];
                    var embedding = await _embeddingService.EmbedAsync(content, cancellationToken);
                    vectorRecords.Add(new RagVectorRecord
                    {
                        ChunkId = Guid.NewGuid(),
                        DocumentId = documentId,
                        ChunkIndex = i,
                        Content = content,
                        Vector = embedding,
                        ContentHash = ComputeHash(content)
                    });
                }

                await _vectorStore.UpsertAsync(vectorRecords, cancellationToken);

                document.Status = RagDocumentStatus.Completed;
                document.ProcessedAtUtc = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                document.Status = RagDocumentStatus.Failed;
                document.ErrorMessage = ex.Message;
                await _context.SaveChangesAsync(cancellationToken);
                throw;
            }
        }

        private static string ComputeHash(string input)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(hash);
        }
    }
}
