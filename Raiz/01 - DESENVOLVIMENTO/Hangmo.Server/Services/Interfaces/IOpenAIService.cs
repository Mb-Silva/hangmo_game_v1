namespace Hangmo.Server.Services.Interfaces
{
    public interface IOpenAIService
    {
        Task<string> GetChatCompletionAsync(string prompt);
    }
}
