namespace Lab04.DTOs
{
    public class AskQuestionRequestDto
    {
        public string Question { get; set; } = string.Empty;
        public int? TopK { get; set; }
        public Guid? DocumentId { get; set; }
    }
}
