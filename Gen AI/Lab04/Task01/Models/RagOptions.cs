namespace Task01.Models;

public sealed class RagOptions
{
    public string UploadFolder { get; set; } = "App_Data/uploads";
    public string ChunkStrategy { get; set; } = "FixedSizeOverlapping";
    public int ChunkSize { get; set; } = 1000;
    public int ChunkOverlap { get; set; } = 180;
    public int EmbeddingDimensions { get; set; } = 256;
    public double MinScore { get; set; } = 0.15;
    public int MaxRetrievedChunks { get; set; } = 8;
}
