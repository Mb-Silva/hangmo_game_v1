using Azure;
using Hangmo.Server.Services.Interfaces;
using Mscc.GenerativeAI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Hangmo.Server.Services
{
    public class GeminiService() : IWordGenerationService
    {

        private GenerativeModel connectIA()
        {
            var apiKey = "AIzaSyBDNoRLHYxY8ieXS_pXV5PVjzYyiCvhjdw";
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15Flash);

            return model;
        }

        public async Task<string> GenerateWordsAsync(string prompt)
        {
            GenerativeModel modelIA = connectIA();

            string exemplojson = "{Exemplo1,Exemplo2,Exemplo3,...}";
            string promptExample = String.Format("Vou te enviar um tema e você deve me retornar uma lista de 20 palavras relacionadas a este tema, mas diferentes daquelas que eu te enviei, para um jogo da forca e as palavras não devem conter erros gramaticais. Essas palavras não podem ser repetidas e conter emojis, nem símbolos. Retorne a lista neste formato json como exemplo: {0} Segue o tema: {1}", exemplojson, prompt);

            var response = await modelIA.GenerateContent(promptExample);
            var json = JsonFormatter(response.Text);

            return JsonFormatter(response.Text);
        }

        private string JsonFormatter(string text)
        {
            // Remover os caracteres de formatação inadequados
            text = text
                .Replace("```json", "")
                .Replace("```", "")
                .Replace("\\n", "")
                .Replace("\\", "")
                .Replace("\"{", "{")
                .Replace("}\"", "}")
                .Replace(@"\n", "")
                .Replace(@"\", "")
                .Trim();

            // Substituir as chaves das palavras para um array válido
            text = text.Replace("{", "[").Replace("}", "]");

            //Incluir o objeto com a nomenclatura "Palavras"
            text = "{ \"palavras\": " + text + " }";

            var jsonObject = JsonConvert.DeserializeObject<JObject>(text);
            var words = jsonObject["palavras"].ToObject<List<string>>();

            // Criar o novo objeto com a estrutura desejada
            var formattedResponse = new
            {
                palavras = words
            };

            string outputJson = JsonConvert.SerializeObject(formattedResponse, Newtonsoft.Json.Formatting.Indented);

            return outputJson;
        }


    }
}
