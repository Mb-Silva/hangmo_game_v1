using Hangmo.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiService _geminiAI;

        public GeminiController(IGeminiService geminiAI)
        {
            _geminiAI = geminiAI;
        }

        [HttpGet(Name = "GetAnswerGemini")]
        public IActionResult Get(string prompt)
        {
            var response = _geminiAI.consultApi(prompt);
            return Ok(response);
        }
    }
}
