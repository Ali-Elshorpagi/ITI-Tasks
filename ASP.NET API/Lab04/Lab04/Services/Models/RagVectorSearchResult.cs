namespace Lab04.Services.Models
{
    public class RagVectorSearchResult
    {
        public Guid ChunkId { get; set; }
        public Guid DocumentId { get; set; }
        public int ChunkIndex { get; set; }
        public string Content { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}
