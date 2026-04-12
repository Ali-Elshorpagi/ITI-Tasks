namespace Task01.Models;

public sealed class MongoDbOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Database { get; set; } = "Task01";
    public string ChatHistoryCollection { get; set; } = "chat_history";
    public string VectorCollection { get; set; } = "rag_vectors";
}
