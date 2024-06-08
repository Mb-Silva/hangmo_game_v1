using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Requests
{
    public class UpdateGameRequest
    {
        public GameStatus? Status { get; set; }
        public GameResult? Result { get; set; }
        public int? WrongGuessCount { get; set; }
        public int? PointsEarned { get; set; }
        


    }
}
