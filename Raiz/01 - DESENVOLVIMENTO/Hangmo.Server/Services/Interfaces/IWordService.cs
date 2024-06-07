using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Services.Interfaces
{
    public interface IWordService
    {
        int GetDailyWord();

        Task<Word> GetWordByGameId(int gameId);
        Task<string> getDecryptedWordByGameId(int gameId);

        Task<Word> GenerateWordByTheme(string theme);

        Task<Word> AddWord(Word word);
    }
}
