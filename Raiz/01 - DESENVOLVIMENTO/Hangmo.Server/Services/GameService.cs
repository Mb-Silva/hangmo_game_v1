using System;
using System.Runtime.CompilerServices;
using Azure.Identity;
using Hangmo.Repository.Data.DAO;
using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Helpers;
using Hangmo.Server.Requests;
using Hangmo.Server.ResponseModels;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Hangmo.Services
{
    public class GameService : IGameService
    {
        private readonly IWordService _wordService;

        private readonly GameDAO _gameDAO;
        

        public GameService(IWordService wordsService, GameDAO gameDAO) 
        { 
            _wordService = wordsService;
            _gameDAO = gameDAO;
        }


        public async Task<MakeGuessResponse> MakeGuess(int gameId, char letter)
        {
            var game = await _gameDAO.GetGameByIdAsync(gameId);

            if (game == null)
            {
                throw new KeyNotFoundException("Game Not Found");
            }

            var word = CryptHelper.Decrypt(game.Word.SecretWord);

            Console.WriteLine(word);

            var guessValidation = await FindLetter(word, letter);

            if (!game.GuessHistory.Contains(letter) && game.Status != GameStatus.Ended){

                if (guessValidation.isPresent)
                {
                    game.RevealedCharactersCount += guessValidation.positions.Count;

                }
                else
                {
                    game.WrongGuessCount++;
                }


                game = DetermineGameResult(game);
                game = DetermineGameStatus(game);
                game.GuessHistory.Add(letter);

                await _gameDAO.UpdateAsync(game);

            }


            MakeGuessResponse response =new MakeGuessResponse
                (
                gameId,
                game.Status, 
                game.Result, 
                game.WrongGuessCount,
                word.Length, 
                guessValidation.isPresent, 
                guessValidation.positions, 
                letter
                );
            
            return response;

        }
        public async Task<(Boolean isPresent, List<(char, int)> positions)> FindLetter (string palavra, char letra)
        {           
            
            // Lista para armazenar as posições onde a letra foi encontrada, juntamente com o caractere encontrado
            List<(char, int)> posicoes = new List<(char, int)>();

            // Converte a palavra e a letra para minúsculas para fazer a comparação sem diferenciar maiúsculas de minúsculas
            palavra = palavra.ToLower();
            letra = char.ToLower(letra);

            // Percorre a palavra para encontrar a letra
            for (int i = 0; i < palavra.Length; i++)
            {
                // Obtém o caractere atual
                char caractere = palavra[i];

                // Verifica se o caractere atual é igual à letra fornecida ou se é um caractere especial correspondente
                if (caractere == letra || CorrespondenteEspecial(caractere, letra))
                {
                    // Se sim, adiciona a posição e o caractere à lista de posições
                    posicoes.Add((caractere, i));
                    
                }
            }

            // Retorna verdadeiro se a letra foi encontrada e falso caso contrário

            
            return (posicoes.Count > 0, posicoes);
        }

        // Função para verificar se um caractere especial corresponde à letra pesquisada
        private static bool CorrespondenteEspecial(char caractere, char letra)
        {
            // Lista de correspondências especiais, por exemplo, 'ã' corresponde a 'a'
            Dictionary<char, char[]> correspondenciasEspeciais = new Dictionary<char, char[]>()
            {
                {'ã', new char[] {'a'}},
                {'ç', new char[] {'c'}},
                {'á', new char[] {'a'}},
                {'à', new char[] {'a'}},
                {'í', new char[] {'i'}},
                {'ì', new char[] {'i'}},
                {'ú', new char[] {'u'}},
                {'ù', new char[] {'u'}},
                {'ó', new char[] {'o'}},
                {'ò', new char[] {'o'}},
                {'é', new char[] {'e'}},
                {'è', new char[] {'e'}},
                // Adicione mais correspondências especiais conforme necessário
            };

            // Verifica se o caractere tem uma correspondência especial para a letra pesquisada
            if (correspondenciasEspeciais.ContainsKey(caractere))
            {
                return Array.Exists(correspondenciasEspeciais[caractere], c => c == letra);
            }

            return false;
        }
        
        
        public async Task<GetGameResponse> GetGameById(int id)
        {
            var game =  await _gameDAO.GetGameByIdAsync(id);

            if (game == null)
            {
                throw new KeyNotFoundException("Game Not Found");
            }

            var wordDecrypt = CryptHelper.Decrypt(game.Word.SecretWord);
            GetGameResponse response = new GetGameResponse(game.Id, game.Status, game.Result, game.WrongGuessCount, wordDecrypt.Length);

            return response;
        }


        public async Task<GetGameResponse> AddGame(string userId, string theme)
        {
            var word = await _wordService.GenerateWordByTheme(theme);
            await _wordService.AddWord(word);

            
            var game = new Game(userId, word.Id);
            await _gameDAO.AddAsync(game);

            var wordDecrypt = CryptHelper.Decrypt(game.Word.SecretWord);
            GetGameResponse response = new GetGameResponse(game.Id, game.Status, game.Result, game.WrongGuessCount, wordDecrypt.Length);

            return response;
        }
        
        public async Task<Game> GetGameByUser(string id)
        {
            return await _gameDAO.GetGameUserActive(id);
        }

        public async Task<Game> EndGame(Game game)
        {
            game.Status = GameStatus.Ended;
            await _gameDAO.UpdateAsync(game);
            return game;
        }


        private Game DetermineGameStatus(Game game) {
            
                        
            if (game.Result != GameResult.None) {
                game.Status = GameStatus.Ended;                            
            }            
            
            return game;

        }

        private Game DetermineGameResult(Game game)
        {
            if (game.RevealedCharactersCount >= CryptHelper.Decrypt(game.Word.SecretWord).Length)
            {
                game.Result = GameResult.Win;
            
            }else if (game.WrongGuessCount >= 6)            
            {
                game.Result = GameResult.Loss;
            }

            return game;
        }


        public async Task DeleteGameById(int id) 
        {  
            await _gameDAO.DeleteAsync(id);
        }

        public async Task<Game?> UpdateGameById(int id, UpdateGameRequest request) 
        {
            var game = await _gameDAO.GetByIdAsync(id);

            if (game == null)
            {
                return null;
            }           

            game.Status = request.Status ?? game.Status;
            game.PointsEarned = request.PointsEarned ?? game.PointsEarned;
            game.WrongGuessCount = request.WrongGuessCount ?? game.WrongGuessCount;
            game.Result = request.Result ?? game.Result;

            await _gameDAO.UpdateAsync(game);

            return game;


        }
    }
}
