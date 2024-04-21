using Hangmo.Server.Services;
using Hangmo.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAI _openAI;

        public OpenAIController(IOpenAI openAI)
        {
            _openAI = openAI;
        }

        [HttpGet("answer")]
        public async Task<IActionResult> Get(string prompt)
        {
            var response = await _openAI.GetChatCompletionAsync(prompt);
            return Ok(response);
        }
    }
}
