using Lab04.DTOs;
using Lab04.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab04.Controllers
{
    [Route("api/rag/documents")]
    [ApiController]
    [AllowAnonymous]
    public class RagDocumentsController : ControllerBase
    {
        private readonly IRagDocumentService _documentService;

        public RagDocumentsController(IRagDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        [RequestSizeLimit(50_000_000)]
        public async Task<ActionResult<UploadDocumentResponseDto>> Upload([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            try
            {
                var result = await _documentService.UploadAndQueueAsync(file, cancellationToken);
                return Accepted(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{documentId:guid}/status")]
        public async Task<ActionResult> GetStatus(Guid documentId, CancellationToken cancellationToken)
        {
            var result = await _documentService.GetStatusAsync(documentId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
