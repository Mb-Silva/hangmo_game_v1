using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Requests;
using Hangmo.Server.ResponseModels;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        Task<GuessResponse> MakeGuess(int gameId, char letter);
        Task<(Boolean isPresent, List<int> positions)> FindLetter(string palavra, char letra);

        Task<Game> GetGameById(int id);
        Task<Game> AddGame(string userId, string theme);

        Task<Game> EndGame(Game game);
        Task DeleteGameById(int id);

        Task<Game?> UpdateGameById(int id, UpdateGameRequest request);
       

    }
}
