namespace Task01.Services.Retrieval;

public sealed class RetrievedChunk
{
    public Guid DocumentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public Guid ChunkId { get; set; }
    public int ChunkIndex { get; set; }
    public string Content { get; set; } = string.Empty;
    public double Score { get; set; }
}
