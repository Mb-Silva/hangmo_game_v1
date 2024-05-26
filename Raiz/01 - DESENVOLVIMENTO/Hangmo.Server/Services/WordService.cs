using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Helpers;
using Hangmo.Server.Services.Interfaces;

namespace Hangmo.Repository.Services
{
    public class WordService : BaseService<Word>, IWordService
    {
        private WordDAO _wordDAO;
        public WordService(IBaseDAO<Word> baseDao, WordDAO wordDao) : base(baseDao)
        {
            _wordDAO = wordDao;
        }

        public int GetDailyWord()
        {
            var cryptHelper = new CryptHelper();
            var wordObject = GetRandomWordByDate(DateTime.Now);

            return cryptHelper.Decrypt(wordObject.SecretWord).Length;
        }

        private Word GetRandomWordByDate(DateTime date)
        {
            var random = new Random();
            var wordList = _wordDAO.ListByDate(date);
            var randomIndex = random.Next(wordList.Count);

            return wordList[randomIndex];
        }
    }
}