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
        private IWordGenerationService _wordGenerationService;
        public WordService(IBaseDAO<Word> baseDao, WordDAO wordDao, IWordGenerationService wordGenerationService) : base(baseDao)
        {
            _wordDAO = wordDao;
            _wordGenerationService = wordGenerationService;
        }

        public int GetDailyWord()
        {
            var wordObject = GetRandomWordByDate(DateTime.Now);
            Console.WriteLine(wordObject.SecretWord);
            return CryptHelper.Decrypt(wordObject.SecretWord).Length;
        }

        private Word GetRandomWordByDate(DateTime date)
        {
            var random = new Random();
            var wordList = _wordDAO.ListByDate(date);
            var randomIndex = random.Next(wordList.Count);

            return wordList[randomIndex];
        }

        public async Task<string> getDecryptedWordByGameId(int gameId)
        {
            var result = await GetWordByGameId(gameId);
            
            return CryptHelper.Decrypt(result.SecretWord);
        }
        
        public async Task<Word> GetWordByGameId(int gameId)
        {
            return await _wordDAO.GetWordByGameId(gameId);
        }

        public async Task<Word> GenerateWordByTheme(string theme)
        {
            string generatedWords =  await _wordGenerationService.GenerateWordsAsync(theme);
            
            var wordList = ParseHelper.Parse(generatedWords);

            var randomWord = ParseHelper.GetRandomFromList<string>(wordList);           

            Word word = new  Word(randomWord);

            return word;
        }


        public async Task<Word> AddWord(Word word)
        {
           
            await _wordDAO.AddAsync(word);
            return word;
        }
    }
}