using Hangmo.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAIService _openAI;

        public OpenAIController(IOpenAIService openAI)
        {
            _openAI = openAI;
        }

        [HttpGet(Name = "GetAnswer")]
        public async Task<IActionResult> Get(string prompt)
        {
            var response = await _openAI.GetChatCompletionAsync(prompt);
            return Ok(response);
        }
    }
}
