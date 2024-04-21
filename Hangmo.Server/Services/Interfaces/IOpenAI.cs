namespace Hangmo.Server.Services.Interfaces
{
    public interface IOpenAI
    {
        Task<string> GetChatCompletionAsync(string prompt);
    }
}
