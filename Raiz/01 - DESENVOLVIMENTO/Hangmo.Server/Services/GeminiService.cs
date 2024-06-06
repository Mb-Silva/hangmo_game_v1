using Hangmo.Server.Services.Interfaces;
using Mscc.GenerativeAI;


namespace Hangmo.Server.Services
{
    public class GeminiService() : IWordGenerationService
    {

        public async Task<string> GenerateWordsAsync(string prompt)
        {
            var apiKey = "AIzaSyBDNoRLHYxY8ieXS_pXV5PVjzYyiCvhjdw";
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15Flash);
            var promptExample = String.Format("Vou te enviar um tema e você deve me retornar uma lista de 10 palavras relacionadas a este tema, mas diferentes daquelas que eu te enviei, para um jogo da forca. As palavras não podem ser repetidas, emojis, nem símbolos. Envie a lista no formato separado por ponto e vírgula, sem espaços. Exemplo de Output esperado: Maçã;Fruta;Verde;Vermelho;Doce;Suco;Computador;Tecnologia;Logo;iPhone Segue o tema: {0}", prompt);

            var response = await model.GenerateContent(promptExample);

            return response.Text;
        }
    }
}
