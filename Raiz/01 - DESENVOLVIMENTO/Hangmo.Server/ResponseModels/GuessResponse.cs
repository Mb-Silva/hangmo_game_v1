using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.ResponseModels
{
    public class GuessResponse
    {
        public GuessResponse(int gameId, GameStatus gameStatus, GameResult gameResult, int wrongGuessCount, int wordLength, bool isPresent, List<int>? positions, char letter)
        {
            GameId = gameId;
            GameStatus = gameStatus;
            GameResult = gameResult;
            WrongGuessCount = wrongGuessCount;
            WordLength = wordLength;
            IsPresent = isPresent;
            Positions = positions;
            Letter = letter;
        }

        public int GameId { get; set; }
        
        public GameStatus GameStatus { get; set; }

        public GameResult GameResult { get; set; } 

        public int WrongGuessCount {  get; set; } 
        
        public int WordLength { get; set; }

        public Boolean IsPresent { get; set; }

        public List<int>? Positions { get; set; }

        public char Letter { get; set; }



    }
}
