using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Requests;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        (bool, List<(int, char)>) FindLetter(int gameId, char letra);

        Task<Game> GetGameById(int id);
        Task<Game> AddGame(string appUserId, int wordId);
        Task DeleteGameById(int id);
        Task<Game?> UpdateGameById(int id, GameUpdateRequest request);

        

    }
}
