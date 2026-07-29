using DJB_Application.Dto;
using DJB_Application.Interface;
using DJB_Core.Interfaces;
using DJB_Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using OpenAI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DJB_Infrastructure.AI
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OpenAIClient _chatClient;

        public OpenAIService(
            IOrderRepository orderRepository,
            IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            string apiKey = configuration["OpenApiOptions:OPENAI_API_KEY"];
            _chatClient = new OpenAIClient(apiKey);
        }
        public async Task<ChatResponse> GenerateAnswerAsync(string question, CancellationToken cancellationToken)
        {
            var chatc = _chatClient.GetChatClient("gpt-4.1-mini");
            var response =
                   await chatc.CompleteChatAsync(question);
            if (response != null)
            {
                ChatResponse chatResponse = new ChatResponse() { Response = response.Value.Content[0].Text };
                return chatResponse;
            }

            return new ChatResponse() { Response = "Sorry, I couldn't determine what information you need." };
        }
        public async Task<ChatResponse> BuildFilterAsync(string question, CancellationToken cancellationToken)
        {
            var lowerPrompt = question.ToLowerInvariant();
            AnalyticsRequest analyticsRequest;
            var chatClientobj = _chatClient.GetOpenAIModelClient();

            if (lowerPrompt.Contains("most ordered") ||
                lowerPrompt.Contains("top selling") ||
                lowerPrompt.Contains("best selling"))
            {
                analyticsRequest = analyticsRequest = new AnalyticsRequest
                {
                    Top = 1,
                    Intent = AnalyticsIntent.MostOrdered,
                    Entity = "Order"
                };
                var orders = await _orderRepository.GetMostOrderedProductsAsync(analyticsRequest, cancellationToken);

                var data = string.Join(Environment.NewLine,
                    orders.Select(x =>
                        $"{x.ProductName} - ProductName :{x.ProductName}"));


                var aiPrompt = $"""
                                    You are an AI sales assistant. 
                                    Using the following data, answer naturally.{data}
                                    most ordered.
                                """;
                //var chatc = _chatClient.GetChatClient("gpt-4.1-mini");
                var chatc = _chatClient.GetChatClient("gpt-4.1-mini");

                var response =
                    await chatc.CompleteChatAsync(aiPrompt);
                analyticsRequest.Answer = response.Value.Content[0].Text;
                //ChatResponse chatResponse = new ChatResponse() { Response = "Kutki is our most ordered product! It's a popular choice among customers for its quality and effectiveness. Would you like to know more about it or place an order?" };
                ChatResponse chatResponse = new ChatResponse() { Response = response.Value.Content[0].Text };
                return chatResponse;
            }

            
            return new ChatResponse() { Response = "Sorry, I couldn't determine what information you need." };
        }

        public Task<string> GenerateAnswerAsync(string question, object data)
        {
            throw new NotImplementedException();
        }

    }
}
