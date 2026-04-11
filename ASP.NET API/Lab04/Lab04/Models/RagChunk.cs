namespace Lab04.Models
{
    public class RagChunk
    {
        public Guid Id { get; set; }
        public Guid RagDocumentId { get; set; }
        public RagDocument? RagDocument { get; set; }
        public int ChunkIndex { get; set; }
        public string Content { get; set; } = string.Empty;
        public string EmbeddingJson { get; set; } = string.Empty;
        public string ContentHash { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
