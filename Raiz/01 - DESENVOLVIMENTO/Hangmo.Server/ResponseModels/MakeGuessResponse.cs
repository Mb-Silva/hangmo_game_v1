using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.ResponseModels
{
    public class LetterCoordinate
    {
        public char Character { get; set; }
        public int Position { get; set; } 
    }
    public class MakeGuessResponse
    {
        public MakeGuessResponse(int gameId, GameStatus gameStatus, GameResult gameResult, int wrongGuessCount, int wordLength, bool isPresent, List<(char,int)> positions, char letter)
        {
            GameId = gameId;
            GameStatus = gameStatus;
            GameResult = gameResult;
            WrongGuessCount = wrongGuessCount;
            WordLength = wordLength;
            IsPresent = isPresent;
            Coordinates = positions.Select(t => new LetterCoordinate {Character = t.Item1, Position = t.Item2}).ToList();
            Letter = letter;
        }

        public int GameId { get; set; }
        
        public GameStatus GameStatus { get; set; }

        public GameResult GameResult { get; set; } 

        public int WrongGuessCount {  get; set; } 
        
        public int WordLength { get; set; }

        public Boolean IsPresent { get; set; }

        public List<LetterCoordinate>? Coordinates{ get; set; }

        public char Letter { get; set; }



    }
}
