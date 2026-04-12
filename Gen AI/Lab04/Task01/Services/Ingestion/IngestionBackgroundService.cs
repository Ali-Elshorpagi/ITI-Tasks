namespace Task01.Services.Ingestion;

public sealed class IngestionBackgroundService : BackgroundService
{
    private readonly IIngestionQueue _queue;
    private readonly IIngestionService _ingestionService;

    public IngestionBackgroundService(IIngestionQueue queue, IIngestionService ingestionService)
    {
        _queue = queue;
        _ingestionService = ingestionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var id = await _queue.DequeueAsync(stoppingToken);
            await _ingestionService.ProcessDocumentAsync(id, stoppingToken);
        }
    }
}
