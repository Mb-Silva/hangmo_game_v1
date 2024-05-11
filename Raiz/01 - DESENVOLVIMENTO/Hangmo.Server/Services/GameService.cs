using System;
using Azure.Identity;
using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;

namespace Hangmo.Services
{
    public class GameService : IGameService
    {
        private readonly IWordsService _wordService;

        public GameService(IWordsService wordsService) 
        { 
            _wordService = wordsService;
        }

        public (bool, List<(int, char)>) FindLetter(string palavra, char letra)
        {
            // Lista para armazenar as posições onde a letra foi encontrada, juntamente com o caractere encontrado
            List<(int, char)> posicoes = new List<(int, char)>();

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
                    posicoes.Add((i, palavra[i]));
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



    }
}
