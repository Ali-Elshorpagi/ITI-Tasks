namespace Lab04.Configurations
{
    public class OpenAiOptions
    {
        public string BaseUrl { get; set; } = "https://api.openai.com";
        public string ApiKey { get; set; } = string.Empty;
        public string EmbeddingModel { get; set; } = "text-embedding-3-small";
        public string ChatModel { get; set; } = "gpt-4o-mini";
    }
}
