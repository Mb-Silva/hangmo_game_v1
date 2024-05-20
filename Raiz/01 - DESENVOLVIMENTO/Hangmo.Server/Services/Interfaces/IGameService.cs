using Hangmo.Repository.Data.Entities;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        (bool, List<(int, char)>) FindLetter(string palavra, char letra);

        Task<Game> GetGameById(int id);
        Task<Game> AddGame(int appUserId, int wordId);
        Task DeleteGameById(int id);
        Task UpdateGameById(int id);

    }
}
