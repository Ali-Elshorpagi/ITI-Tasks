namespace Task01.Services.Retrieval;

public interface IRetrievalService
{
    Task<IReadOnlyList<RetrievedChunk>> RetrieveAsync(string question, int topK, CancellationToken ct);
}
