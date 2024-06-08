using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.ResponseModels
{
    public class GetGameResponse
    {
        public GetGameResponse(int gameId, GameStatus gameStatus, GameResult gameResult, int wrongGuessCount, int wordLength)
        {
            GameId = gameId;
            GameStatus = gameStatus;
            GameResult = gameResult;
            WrongGuessCount = wrongGuessCount;
            WordLength = wordLength;
        }

        public int GameId { get; set; }

        public GameStatus GameStatus { get; set; }

        public GameResult GameResult { get; set; }

        public int WrongGuessCount { get; set; }

        public int WordLength { get; set; }

    }
}
