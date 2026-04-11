namespace Lab04.Services.Contracts
{
    public interface IEmbeddingService
    {
        Task<float[]> EmbedAsync(string text, CancellationToken cancellationToken);
    }
}
