namespace Task01.Services.Chunking;

public sealed class ChunkSegment
{
    public int Index { get; set; }
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public string Content { get; set; } = string.Empty;
}
