namespace Task01.Models;

public sealed class AskResponse
{
    public string Answer { get; set; } = string.Empty;
    public double RelevanceScore { get; set; }
    public List<SourceCitation> Sources { get; set; } = [];
}

public sealed class SourceCitation
{
    public Guid DocumentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public int ChunkIndex { get; set; }
    public double Score { get; set; }
}
