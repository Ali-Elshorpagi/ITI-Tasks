using Task01.Data.Entities;
using Task01.Data.Repositories;
using Task01.Models;
using Task01.Services.Chunking;
using Task01.Services.Embedding;
using Task01.Services.Parsing;
using Microsoft.Extensions.Options;

namespace Task01.Services.Ingestion;

public sealed class IngestionService : IIngestionService
{
    private readonly IEnumerable<IDocumentParser> _parsers;
    private readonly IDocumentRepository _documentRepository;
    private readonly IChunkRepository _chunkRepository;
    private readonly IVectorRepository _vectorRepository;
    private readonly IEmbeddingService _embeddingService;
    private readonly IChunker _chunker;
    private readonly IIngestionQueue _queue;
    private readonly IWebHostEnvironment _environment;
    private readonly RagOptions _options;

    public IngestionService(
        IEnumerable<IDocumentParser> parsers,
        IDocumentRepository documentRepository,
        IChunkRepository chunkRepository,
        IVectorRepository vectorRepository,
        IEmbeddingService embeddingService,
        IChunker chunker,
        IIngestionQueue queue,
        IWebHostEnvironment environment,
        IOptions<RagOptions> options)
    {
        _parsers = parsers;
        _documentRepository = documentRepository;
        _chunkRepository = chunkRepository;
        _vectorRepository = vectorRepository;
        _embeddingService = embeddingService;
        _chunker = chunker;
        _queue = queue;
        _environment = environment;
        _options = options.Value;
    }

    public async Task<UploadResult> EnqueueUploadAsync(IFormFile file, CancellationToken ct)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var parser = _parsers.FirstOrDefault(p => p.CanParse(extension));
        if (parser is null)
        {
            throw new InvalidOperationException($"No parser found for extension {extension}.");
        }

        var documentId = Guid.NewGuid();
        var root = Path.Combine(_environment.ContentRootPath, _options.UploadFolder);
        Directory.CreateDirectory(root);

        var filePath = Path.Combine(root, $"{documentId}{extension}");
        await using (var stream = File.Create(filePath))
        {
            await file.CopyToAsync(stream, ct);
        }

        await _documentRepository.AddAsync(new DocumentEntity
        {
            Id = documentId,
            FileName = file.FileName,
            Extension = extension,
            Path = filePath,
            Status = DocumentStatus.Queued
        }, ct);

        await _queue.EnqueueAsync(documentId, ct);

        return new UploadResult { DocumentId = documentId, Status = "Queued" };
    }

    public async Task ProcessDocumentAsync(Guid documentId, CancellationToken ct)
    {
        var document = await _documentRepository.GetAsync(documentId, ct);
        if (document is null)
        {
            return;
        }

        try
        {
            document.Status = DocumentStatus.Processing;
            await _documentRepository.UpdateAsync(document, ct);

            var parser = _parsers.First(p => p.CanParse(document.Extension));
            await using var stream = File.OpenRead(document.Path);
            var raw = await parser.ParseAsync(stream, ct);
            var text = TextPreprocessor.Normalize(raw);

            var chunks = _chunker.Chunk(text, _options.ChunkSize, _options.ChunkOverlap);
            var embeddings = await _embeddingService.EmbedBatchAsync(chunks.Select(c => c.Content).ToList(), ct);

            var chunkEntities = new List<ChunkEntity>(chunks.Count);
            var vectorEntities = new List<VectorRecord>(chunks.Count);

            for (var i = 0; i < chunks.Count; i++)
            {
                var chunkId = Guid.NewGuid();
                var chunk = chunks[i];

                chunkEntities.Add(new ChunkEntity
                {
                    Id = chunkId,
                    DocumentId = documentId,
                    ChunkIndex = chunk.Index,
                    StartOffset = chunk.StartOffset,
                    EndOffset = chunk.EndOffset,
                    Content = chunk.Content
                });

                vectorEntities.Add(new VectorRecord
                {
                    ChunkId = chunkId,
                    DocumentId = documentId,
                    Vector = embeddings[i]
                });
            }

            await _chunkRepository.AddRangeAsync(chunkEntities, ct);
            await _vectorRepository.UpsertAsync(vectorEntities, ct);

            document.Status = DocumentStatus.Completed;
            document.Error = null;
            await _documentRepository.UpdateAsync(document, ct);
        }
        catch (Exception ex)
        {
            document.Status = DocumentStatus.Failed;
            document.Error = ex.Message;
            await _documentRepository.UpdateAsync(document, ct);
        }
    }
}
