using DJB_Application.Dto;
using DJB_Core.Models;

namespace DJB_Application.Interface
{
    public interface IOpenAIService
    {

        Task<ChatResponse> BuildFilterAsync(string question, CancellationToken cancellationToken);

        Task<string> GenerateAnswerAsync(string question, object data);
    }
}
