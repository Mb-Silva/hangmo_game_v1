using Hangmo.Server.Helpers;
using Hangmo.Server.Services.Interfaces;
using Mscc.GenerativeAI;


namespace Hangmo.Server.Services
{
    public class GeminiService(IConfiguration configuration) : IWordGenerationService
    {
        private readonly string apiKey = configuration["AI:ApiKey"].ToString();
        private readonly string promptModel = configuration["AI:PromptModel"].ToString();

        private GenerativeModel GenerativeModelConnect()
        {
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15Flash);

            return model;
        }

        public async Task<string> GenerateWordsAsync(string prompt)
        {
            var modelAI = GenerativeModelConnect();
            var finalPrompt = String.Format("{0} {1}", promptModel, prompt);

            var response = await modelAI.GenerateContent(finalPrompt);
            var parsed = ParseHelper.Parse(response.Text);

            foreach (var item in parsed)
            {
                Console.WriteLine(item);
            }

            return response.Text;
        }

    }
}
