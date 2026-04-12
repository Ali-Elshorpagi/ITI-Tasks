namespace Task01.Models;

public sealed class VectorStoreOptions
{
    public string Provider { get; set; } = "Chroma";
    public string ChromaUrl { get; set; } = "http://localhost:8000";
    public string ChromaCollection { get; set; } = "rag_chunks";

    public string QdrantUrl { get; set; } = "http://localhost:6333";
    public string QdrantApiKey { get; set; } = string.Empty;
    public string QdrantCollection { get; set; } = "rag_chunks";
}
