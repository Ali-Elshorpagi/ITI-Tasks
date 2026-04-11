using Lab04.DTOs;

namespace Lab04.Services.Contracts
{
    public interface IRagQueryService
    {
        Task<AskQuestionResponseDto> AskAsync(AskQuestionRequestDto request, CancellationToken cancellationToken);
    }
}
