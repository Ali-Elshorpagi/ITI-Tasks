namespace Task01.Services.Chunking;

public sealed class RecursiveChunker : IChunker
{
    public IReadOnlyList<ChunkSegment> Chunk(string text, int chunkSize, int overlap)
    {
        var chunks = new List<ChunkSegment>();
        if (string.IsNullOrWhiteSpace(text))
        {
            return chunks;
        }

        var index = 0;
        var start = 0;

        while (start < text.Length)
        {
            var end = Math.Min(text.Length, start + chunkSize);

            if (end < text.Length)
            {
                var adjusted = FindNaturalBreak(text, start, end);
                if (adjusted > start)
                {
                    end = adjusted;
                }
            }

            var content = text[start..end].Trim();
            if (content.Length == 0)
            {
                break;
            }

            chunks.Add(new ChunkSegment
            {
                Index = index++,
                StartOffset = start,
                EndOffset = end,
                Content = content
            });

            if (end >= text.Length)
            {
                break;
            }

            var nextStart = Math.Max(0, end - overlap);
            if (nextStart <= start)
            {
                nextStart = end;
            }

            start = nextStart;
        }

        return chunks;
    }

    private static int FindNaturalBreak(string text, int start, int end)
    {
        var searchStart = Math.Max(start, end - 120);
        for (var i = end - 1; i >= searchStart; i--)
        {
            var c = text[i];
            if (c is '.' or '?' or '!' or '\n')
            {
                return i + 1;
            }
        }

        for (var i = end - 1; i >= searchStart; i--)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                return i + 1;
            }
        }

        return end;
    }
}
