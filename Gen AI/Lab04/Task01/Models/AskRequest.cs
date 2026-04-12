namespace Task01.Models;

public sealed class AskRequest
{
    public string Question { get; set; } = string.Empty;
    public int TopK { get; set; } = 8;
}
