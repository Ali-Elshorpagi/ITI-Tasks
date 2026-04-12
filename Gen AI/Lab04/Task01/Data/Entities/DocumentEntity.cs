namespace Task01.Data.Entities;

public sealed class DocumentEntity
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DocumentStatus Status { get; set; } = DocumentStatus.Queued;
    public string? Error { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
}
