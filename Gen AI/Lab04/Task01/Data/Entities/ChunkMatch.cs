namespace Task01.Data.Entities;

public sealed class ChunkMatch
{
    public Guid ChunkId { get; set; }
    public double SemanticScore { get; set; }
    public double LexicalScore { get; set; }
    public double CombinedScore { get; set; }
}
