namespace Hangmo.Server.Services.Interfaces
{
    public interface IWordGenerationService
    {
        public Task<string> GenerateWordsAsync(string prompt);
    }
}
