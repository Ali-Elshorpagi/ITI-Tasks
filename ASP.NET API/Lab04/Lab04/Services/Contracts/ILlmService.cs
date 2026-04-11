namespace Lab04.Services.Contracts
{
    public interface ILlmService
    {
        Task<string> GenerateGroundedAnswerAsync(string question, IReadOnlyList<string> contexts, CancellationToken cancellationToken);
    }
}
