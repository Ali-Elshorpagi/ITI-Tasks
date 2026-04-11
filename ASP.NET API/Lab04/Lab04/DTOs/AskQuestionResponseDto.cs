namespace Lab04.DTOs
{
    public class AskQuestionResponseDto
    {
        public string Answer { get; set; } = string.Empty;
        public List<RetrievedChunkDto> Sources { get; set; } = new();
    }

    public class RetrievedChunkDto
    {
        public Guid DocumentId { get; set; }
        public Guid ChunkId { get; set; }
        public int ChunkIndex { get; set; }
        public double Score { get; set; }
        public string Preview { get; set; } = string.Empty;
    }
}
