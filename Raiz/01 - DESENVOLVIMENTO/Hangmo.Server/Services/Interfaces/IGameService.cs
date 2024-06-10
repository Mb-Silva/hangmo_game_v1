using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Requests;
using Hangmo.Server.ResponseModels;

namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        Task<MakeGuessResponse> MakeGuess(int gameId, char letter);
        Task<(Boolean isPresent, List<(char, int)> positions)> FindLetter(string palavra, char letra);

        Task<GetGameResponse> GetGameById(int id);
        Task<GetGameResponse> AddGame(string userId, string theme);

        Task<Game> EndGame(Game game);
        Task DeleteGameById(int id);

        Task<Game?> UpdateGameById(int id, UpdateGameRequest request);
       

    }
}
