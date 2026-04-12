using Microsoft.AspNetCore.Mvc;
using Task01.Data.Repositories;
using Task01.Services.Ingestion;

namespace Task01.Controllers;

[ApiController]
[Route("documents")]
public sealed class DocumentsController : ControllerBase
{
    private static readonly string[] Allowed = [".pdf", ".txt", ".md", ".docx", ".csv"];
    private readonly IIngestionService _ingestionService;
    private readonly IDocumentRepository _documentRepository;
    private readonly IVectorRepository _vectorRepository;

    public DocumentsController(IIngestionService ingestionService, IDocumentRepository documentRepository, IVectorRepository vectorRepository)
    {
        _ingestionService = ingestionService;
        _documentRepository = documentRepository;
        _vectorRepository = vectorRepository;
    }

    [HttpPost("upload")]
    [RequestSizeLimit(50_000_000)]
    public async Task<IActionResult> Upload(IFormFile? file, CancellationToken ct)
    {
        if (file is null)
        {
            return BadRequest("Please choose a file before uploading.");
        }

        if (file.Length == 0)
        {
            return BadRequest("The selected file is empty.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!Allowed.Contains(extension))
        {
            return BadRequest($"Unsupported file format: {extension}");
        }

        var result = await _ingestionService.EnqueueUploadAsync(file, ct);
        return Accepted(result);
    }

    [HttpGet("{documentId:guid}")]
    public async Task<IActionResult> GetStatus(Guid documentId, CancellationToken ct)
    {
        var document = await _documentRepository.GetAsync(documentId, ct);
        if (document is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            documentId = document.Id,
            fileName = document.FileName,
            status = document.Status.ToString(),
            error = document.Error,
            updatedUtc = document.UpdatedUtc
        });
    }

    [HttpGet("{documentId:guid}/vectors")]
    public async Task<IActionResult> GetVectors(Guid documentId, CancellationToken ct)
    {
        var document = await _documentRepository.GetAsync(documentId, ct);
        if (document is null)
        {
            return NotFound();
        }

        var vectors = await _vectorRepository.GetByDocumentIdAsync(documentId, ct);
        return Ok(vectors.Select(v => new
        {
            chunkId = v.ChunkId,
            documentId = v.DocumentId,
            dimensions = v.Vector.Length,
            preview = v.Vector.Take(8).Select(x => Math.Round(x, 4)).ToArray(),
            magnitude = Math.Round(Math.Sqrt(v.Vector.Sum(x => x * x)), 4)
        }).ToList());
    }
}
