namespace Hangmo.Services.Interfaces
{
    public interface IGameService
    {
        (bool, List<(int, char)>) FindLetter(string palavra, char letra);
    }
}
