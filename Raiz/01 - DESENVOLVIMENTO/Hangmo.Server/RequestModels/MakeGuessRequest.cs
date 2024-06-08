namespace Hangmo.Server.Requests
{
    public class MakeGuessRequest
    {
        public int GameId { get; set; }
        public char Letter { get; set; }
    }
}
