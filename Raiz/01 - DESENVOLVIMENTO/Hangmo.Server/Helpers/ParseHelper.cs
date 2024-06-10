using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Helpers
{
    public static class ParseHelper
    {
        public static List<string> Parse(string input)
        {
            return input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        } 

        public static T GetRandomFromList<T>(List<T> list)
        {
            Random random = new Random();
            T randomItem = list.OrderBy(x => random.Next()).First();

            
            return randomItem;
        }
    }
}
