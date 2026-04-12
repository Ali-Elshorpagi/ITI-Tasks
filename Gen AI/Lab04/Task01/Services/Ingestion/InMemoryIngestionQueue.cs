using System.Threading.Channels;

namespace Task01.Services.Ingestion;

public sealed class InMemoryIngestionQueue : IIngestionQueue
{
    private readonly Channel<Guid> _channel = Channel.CreateUnbounded<Guid>();

    public ValueTask EnqueueAsync(Guid documentId, CancellationToken ct)
    {
        return _channel.Writer.WriteAsync(documentId, ct);
    }

    public ValueTask<Guid> DequeueAsync(CancellationToken ct)
    {
        return _channel.Reader.ReadAsync(ct);
    }
}
