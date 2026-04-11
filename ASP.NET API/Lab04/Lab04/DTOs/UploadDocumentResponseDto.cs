namespace Lab04.DTOs
{
    public class UploadDocumentResponseDto
    {
        public Guid DocumentId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
