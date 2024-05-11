using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Helpers;
using Hangmo.Server.Services.Interfaces;

namespace Hangmo.Repository.Services
{
    public class WordsService : BaseService<Words>, IWordsService
    {
        private WordsDAO _wordsDAO;
        public WordsService(IBaseDAO<Words> baseDao, WordsDAO wordsDao) : base(baseDao)
        {
            _wordsDAO = wordsDao;
        }

        public int GetDailyWord()
        {
            var cryptHelper = new CryptHelper();
            var wordObject = GetRandomWordByDate(DateTime.Now);

            return cryptHelper.Decrypt(wordObject.Word).Length;
        }

        private Words GetRandomWordByDate(DateTime date)
        {
            var random = new Random();
            var wordsList = _wordsDAO.ListByDate(date);
            var randomIndex = random.Next(wordsList.Count);

            return wordsList[randomIndex];
        }
    }
}