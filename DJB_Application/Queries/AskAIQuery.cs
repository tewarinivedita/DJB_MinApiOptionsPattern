using DJB_Application.Dto;
using DJB_Application.Interface;
using DJB_Core.DataTransferObjects;
using DJB_Core.Interfaces;
using DJB_Core.Models;
using MediatR;

namespace DJB_Application.Queries
{
    public record AskAnalyticsQuery(string Question) : IRequest<ChatResponse>;
    public class AskAnalyticsQueryHandler : IRequestHandler<AskAnalyticsQuery, ChatResponse>
    {
        private readonly IOpenAIService _ai;
        private readonly IOrderRepository _orderAnalytics;
    
        public AskAnalyticsQueryHandler(IOpenAIService ai, IOrderRepository orderRepository)
        {
            _ai = ai;
            _orderAnalytics = orderRepository;
        }

        public async Task<ChatResponse> Handle(AskAnalyticsQuery request, CancellationToken cancellationToken)
        {
            ChatResponse chatResponse = await _ai.BuildFilterAsync(request.Question, cancellationToken);

            return chatResponse;

        }
    }
}
