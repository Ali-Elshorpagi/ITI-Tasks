using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Task01.Models;
using Task01.Services.Generation;
using Task01.Services.Retrieval;

namespace Task01.Controllers;

[ApiController]
[Route("chat")]
public sealed class ChatController : ControllerBase
{
    private readonly IRetrievalService _retrievalService;
    private readonly IAnswerGenerator _answerGenerator;

    public ChatController(IRetrievalService retrievalService, IAnswerGenerator answerGenerator)
    {
        _retrievalService = retrievalService;
        _answerGenerator = answerGenerator;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<AskResponse>> Ask([FromBody] AskRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Question is required.");
        }

        var chunks = await _retrievalService.RetrieveAsync(request.Question, request.TopK, ct);
        var response = await _answerGenerator.GenerateAsync(request.Question, chunks, ct);
        response.RelevanceScore = CalculateRelevanceScore(request.Question, chunks);
        response.Sources = chunks.Select(c => new SourceCitation
        {
            DocumentId = c.DocumentId,
            FileName = c.FileName,
            ChunkIndex = c.ChunkIndex,
            Score = c.Score
        }).ToList();

        return Ok(response);
    }

    private static double CalculateRelevanceScore(string question, IReadOnlyList<RetrievedChunk> chunks)
    {
        if (chunks.Count == 0)
        {
            return 0d;
        }

        var topChunks = chunks.Take(3).ToList();
        var semantic = Math.Clamp(topChunks.Average(c => c.Score), 0d, 1d);
        var boostedSemantic = Math.Clamp(semantic * 1.35d, 0d, 1d);

        var queryTokens = Tokenize(question);
        var coverage = 0d;

        if (queryTokens.Count > 0)
        {
            coverage = topChunks
                .Select(chunk =>
                {
                    var chunkTokens = Tokenize(chunk.Content);
                    var hits = queryTokens.Count(t => chunkTokens.Contains(t));
                    return (double)hits / queryTokens.Count;
                })
                .Max();
        }

        var blended = (boostedSemantic * 0.7d) + (coverage * 0.3d);
        return Math.Round(Math.Clamp(blended, 0d, 1d) * 100d, 1);
    }

    private static HashSet<string> Tokenize(string input)
    {
        return Regex.Matches(input.ToLowerInvariant(), "[a-z0-9]+")
            .Select(m => m.Value)
            .ToHashSet();
    }
}
