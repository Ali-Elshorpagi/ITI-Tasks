namespace Task01.Services.Chunking;

public interface IChunker
{
    IReadOnlyList<ChunkSegment> Chunk(string text, int chunkSize, int overlap);
}
