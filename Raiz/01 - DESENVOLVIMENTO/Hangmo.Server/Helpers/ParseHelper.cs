using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Helpers
{
    public static class ParseHelper
    {
        public static List<string> Parse(string input)
        {
            var result = new List<string>();

            string[] secretWords = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in secretWords)
            {
                result.Add(word);
            }
            
            return result;
        } 
    }
}
