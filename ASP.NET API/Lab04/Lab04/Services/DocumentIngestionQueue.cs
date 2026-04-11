using System.Threading.Channels;
using Lab04.Services.Contracts;

namespace Lab04.Services
{
    public class DocumentIngestionQueue : IDocumentIngestionQueue
    {
        private readonly Channel<Guid> _queue = Channel.CreateUnbounded<Guid>();

        public ValueTask QueueAsync(Guid documentId, CancellationToken cancellationToken)
        {
            return _queue.Writer.WriteAsync(documentId, cancellationToken);
        }

        public ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken)
        {
            return _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
