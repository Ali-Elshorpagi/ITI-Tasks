namespace Task01.Data.Entities;

public sealed class ChunkEntity
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public int ChunkIndex { get; set; }
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public string Content { get; set; } = string.Empty;
}
