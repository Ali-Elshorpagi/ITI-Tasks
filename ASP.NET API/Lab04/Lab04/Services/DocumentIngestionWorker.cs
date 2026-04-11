using Lab04.Services.Contracts;

namespace Lab04.Services
{
    public class DocumentIngestionWorker : BackgroundService
    {
        private readonly IDocumentIngestionQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DocumentIngestionWorker> _logger;

        public DocumentIngestionWorker(IDocumentIngestionQueue queue, IServiceScopeFactory scopeFactory, ILogger<DocumentIngestionWorker> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var documentId = await _queue.DequeueAsync(stoppingToken);
                    using var scope = _scopeFactory.CreateScope();
                    var ingestionService = scope.ServiceProvider.GetRequiredService<IDocumentIngestionService>();
                    await ingestionService.IngestAsync(documentId, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RAG document ingestion failed in worker loop.");
                }
            }
        }
    }
}
