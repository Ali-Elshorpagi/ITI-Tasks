namespace Lab04.Services.Models
{
    public class RagVectorRecord
    {
        public Guid ChunkId { get; set; }
        public Guid DocumentId { get; set; }
        public int ChunkIndex { get; set; }
        public string Content { get; set; } = string.Empty;
        public float[] Vector { get; set; } = Array.Empty<float>();
        public string ContentHash { get; set; } = string.Empty;
    }
}
