using Lab04.Configurations;
using Lab04.Services.Contracts;
using Microsoft.Extensions.Options;

namespace Lab04.Services
{
    public class SlidingWindowChunkingService : IChunkingService
    {
        private readonly RagOptions _options;

        public SlidingWindowChunkingService(IOptions<RagOptions> options)
        {
            _options = options.Value;
        }

        public IReadOnlyList<string> Chunk(string normalizedText)
        {
            if (string.IsNullOrWhiteSpace(normalizedText))
            {
                return Array.Empty<string>();
            }

            var words = normalizedText.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (words.Length == 0)
            {
                return Array.Empty<string>();
            }

            var chunkSize = Math.Max(40, _options.ChunkSizeWords);
            var overlap = Math.Clamp(_options.ChunkOverlapWords, 0, chunkSize - 1);
            var step = chunkSize - overlap;

            var chunks = new List<string>();
            for (var i = 0; i < words.Length; i += step)
            {
                var taken = words.Skip(i).Take(chunkSize).ToArray();
                if (taken.Length == 0)
                {
                    break;
                }

                chunks.Add(string.Join(' ', taken));
                if (i + chunkSize >= words.Length)
                {
                    break;
                }
            }

            return chunks;
        }
    }
}
