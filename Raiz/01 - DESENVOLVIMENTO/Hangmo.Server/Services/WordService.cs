using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Helpers;
using Hangmo.Server.Services.Interfaces;
using Microsoft.Identity.Client;

namespace Hangmo.Repository.Services
{
    public class WordService : BaseService<Word>, IWordService
    {
        private WordDAO _wordDAO;
        private ICryptHelper _cryptHelper;
        public WordService(IBaseDAO<Word> baseDao, WordDAO wordDao, ICryptHelper cryptHelper) : base(baseDao)
        {
            _wordDAO = wordDao;
            _cryptHelper = cryptHelper;
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

        public string getDecryptedWordByGameId(int gameId)
        {
            return _cryptHelper.Decrypt(GetWordByGameId(gameId).SecretWord);
        }
        public Word GetWordByGameId(int gameId)
        {
            return _wordDAO.GetWordByGameId(gameId);
        }
    }
}