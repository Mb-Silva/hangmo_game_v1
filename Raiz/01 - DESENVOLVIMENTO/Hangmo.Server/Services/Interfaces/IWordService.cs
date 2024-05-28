using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Services.Interfaces
{
    public interface IWordService
    {
        int GetDailyWord();

        Word GetWordByGameId(int gameId);
        string getDecryptedWordByGameId(int gameId);
    }
}
