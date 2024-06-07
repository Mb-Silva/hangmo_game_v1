using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Requests;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        Task<(bool, List<(int, char)>)> FindLetter(int gameId, char letra);

        Task<Game> GetGameById(int id);
        Task<Game> AddGame(string userId, string theme);
        Task DeleteGameById(int id);

        Task<Game?> UpdateGameById(int id, UpdateGameRequest request);
       

    }
}
