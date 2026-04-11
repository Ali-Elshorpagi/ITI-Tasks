namespace Lab04.Models
{
    public class RagDocument
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty;
        public RagDocumentStatus Status { get; set; } = RagDocumentStatus.Uploaded;
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAtUtc { get; set; }
        public ICollection<RagChunk> Chunks { get; set; } = new List<RagChunk>();
    }
}
