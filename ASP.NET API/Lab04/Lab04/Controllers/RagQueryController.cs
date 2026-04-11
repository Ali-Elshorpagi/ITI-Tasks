using Lab04.DTOs;
using Lab04.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab04.Controllers
{
    [Route("api/rag/query")]
    [ApiController]
    [AllowAnonymous]
    public class RagQueryController : ControllerBase
    {
        private readonly IRagQueryService _ragQueryService;

        public RagQueryController(IRagQueryService ragQueryService)
        {
            _ragQueryService = ragQueryService;
        }

        [HttpPost("ask")]
        public async Task<ActionResult<AskQuestionResponseDto>> Ask([FromBody] AskQuestionRequestDto request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _ragQueryService.AskAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
