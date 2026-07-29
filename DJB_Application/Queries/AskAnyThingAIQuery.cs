using DJB_Application.Dto;
using DJB_Application.Interface;
using DJB_Core.DataTransferObjects;
using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record AskAnyThingAIQuery(string Question) : IRequest<ChatResponse>;
    public class AskAnyThingAIQueryHandler : IRequestHandler<AskAnyThingAIQuery, ChatResponse>
    {
        private readonly IOpenAIService _ai;
    
        public AskAnyThingAIQueryHandler(IOpenAIService ai)
        {
            _ai = ai;
        }

        public async Task<ChatResponse> Handle(AskAnyThingAIQuery request, CancellationToken cancellationToken)
        {
            ChatResponse chatResponse = await _ai.GenerateAnswerAsync(request.Question, cancellationToken);

            return chatResponse;

        }
    }
}
