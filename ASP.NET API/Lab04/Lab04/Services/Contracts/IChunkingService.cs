namespace Lab04.Services.Contracts
{
    public interface IChunkingService
    {
        IReadOnlyList<string> Chunk(string normalizedText);
    }
}
