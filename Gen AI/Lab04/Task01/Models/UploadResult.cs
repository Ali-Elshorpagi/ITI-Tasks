namespace Task01.Models;

public sealed class UploadResult
{
    public Guid DocumentId { get; set; }
    public string Status { get; set; } = "Queued";
}
