using Task01.Models;

namespace Task01.Services
{
    public interface IAIService
    {
        Task<AIResponse> AskQuestionAsync(QuestionRequest request);
    }
}
