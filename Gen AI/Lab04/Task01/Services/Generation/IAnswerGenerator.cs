using Task01.Models;
using Task01.Services.Retrieval;

namespace Task01.Services.Generation;

public interface IAnswerGenerator
{
    Task<AskResponse> GenerateAsync(string question, IReadOnlyList<RetrievedChunk> chunks, CancellationToken ct);
}
