using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Hangmo.Repository.Data.Entities;
using Hangmo.Repository.Services;
using Hangmo.Server.Helpers;
using Hangmo.Server.Repository.Models;

namespace Hangmo.Server.Services.HostedServices
{
    public class WordGenerationService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WordGenerationService(IServiceScopeFactory serviceScopeFactory, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Aguarda X tempo antes de gerar a próxima palavra
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;

                    var cryptHelper = new CryptHelper();

                    // Resolve o BaseService<Words> dentro do escopo
                    var wordsService = serviceProvider.GetRequiredService<BaseService<Word>>();

                    // Geração da palavra usando a API OpenAI
                    string word = await GenerateWordAsync();
                    var valueCrypt = cryptHelper.Crypt(word);

                    var wordModel = new Word();
                    wordModel.Date = DateTime.UtcNow;
                    wordModel.SecretWord = valueCrypt;

                    try
                    {
                        await wordsService.AddAsync(wordModel);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
            }
        }

        private async Task<string> GenerateWordAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("ChtpGPT");
            string prompt = _configuration["AI:Prompt"] ?? "";

            if (prompt == "")
                return "";

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

            try
            {
                using var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                httpReq.Headers.Add("Authorization", $"Bearer {_configuration["AI:ApiKey"]}");

                string requestString = JsonSerializer.Serialize(completionRequest);
                httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

                using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
                httpResponse.EnsureSuccessStatusCode();

                var completionResponse = httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<ChatCompletionResponse>(await httpResponse.Content.ReadAsStringAsync()) : null;

                return completionResponse?.Choices?[0]?.Message?.Content;
            }
            catch
            {
                return "";
            }
        }
    }
}