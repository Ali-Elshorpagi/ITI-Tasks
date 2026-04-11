using Lab04.Services.Models;

namespace Lab04.Services.Contracts
{
    public interface IRagReranker
    {
        IReadOnlyList<RagVectorSearchResult> Rerank(string question, IReadOnlyList<RagVectorSearchResult> candidates);
    }
}
