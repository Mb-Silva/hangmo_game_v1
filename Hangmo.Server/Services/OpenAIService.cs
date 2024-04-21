using Hangmo.Server.Repository.Models;
using Hangmo.Server.Services.Interfaces;
using OpenAI;
using OpenAI.Chat;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Hangmo.Server.Services
{
    public class OpenAIService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : IOpenAI
    {
        public async Task<string> GetChatCompletionAsync(string prompt)
        {
            var httpClient = httpClientFactory.CreateClient("ChtpGPT");

            Repository.Models.OpenAI completionRequest = new()
            {
                Model = "gpt-3.5-turbo",
                MaxTokens = 1000,
                Messages = [
                                new Repository.Models.Message()
                                {
                                    Role = "user",
                                    Content = prompt,
                                }
                            ]
            };

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            httpReq.Headers.Add("Authorization", $"Bearer {configuration["ApiKey"]}");

            string requestString = JsonSerializer.Serialize(completionRequest);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
            httpResponse.EnsureSuccessStatusCode();

            var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;

            return completionResponse.Choices?[0]?.Message?.Content;
        }
    }
}
