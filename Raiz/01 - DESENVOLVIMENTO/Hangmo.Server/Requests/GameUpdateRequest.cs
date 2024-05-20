using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Requests
{
    public class GameUpdateRequest
    {
        public GameStatus? Status { get; set; }
        public GameResult? Result { get; set; }
        public int? GuessCount { get; set; }
        public int? PointsEarned { get; set; }
        


    }
}
