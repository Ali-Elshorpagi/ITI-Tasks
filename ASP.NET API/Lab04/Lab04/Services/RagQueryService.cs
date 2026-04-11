using Lab04.Configurations;
using Lab04.DTOs;
using Lab04.Services.Contracts;
using Microsoft.Extensions.Options;

namespace Lab04.Services
{
    public class RagQueryService : IRagQueryService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly IVectorStore _vectorStore;
        private readonly IRagReranker _reranker;
        private readonly ILlmService _llmService;
        private readonly RagOptions _options;

        public RagQueryService(
            IEmbeddingService embeddingService,
            IVectorStore vectorStore,
            IRagReranker reranker,
            ILlmService llmService,
            IOptions<RagOptions> options)
        {
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
            _reranker = reranker;
            _llmService = llmService;
            _options = options.Value;
        }

        public async Task<AskQuestionResponseDto> AskAsync(AskQuestionRequestDto request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                throw new ArgumentException("Question is required.");
            }

            var topK = request.TopK.GetValueOrDefault(_options.DefaultTopK);
            topK = Math.Clamp(topK, 1, 50);

            var queryVector = await _embeddingService.EmbedAsync(request.Question, cancellationToken);
            var retrieved = await _vectorStore.SearchAsync(queryVector, topK, request.DocumentId, cancellationToken);
            var reranked = _reranker.Rerank(request.Question, retrieved);

            var contextLimit = Math.Clamp(_options.DefaultContextChunks, 1, 20);
            var finalContexts = reranked.Take(contextLimit).ToList();
            var answer = await _llmService.GenerateGroundedAnswerAsync(request.Question, finalContexts.Select(x => x.Content).ToList(), cancellationToken);

            return new AskQuestionResponseDto
            {
                Answer = answer,
                Sources = finalContexts.Select(x => new RetrievedChunkDto
                {
                    DocumentId = x.DocumentId,
                    ChunkId = x.ChunkId,
                    ChunkIndex = x.ChunkIndex,
                    Score = x.Score,
                    Preview = x.Content.Length <= 180 ? x.Content : x.Content[..180] + "..."
                }).ToList()
            };
        }
    }
}
