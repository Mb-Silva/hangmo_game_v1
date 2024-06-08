namespace Hangmo.Server.Requests
{
    public class GuessRequest
    {
        public int GameId { get; set; }
        public char Letter { get; set; }
    }
}
