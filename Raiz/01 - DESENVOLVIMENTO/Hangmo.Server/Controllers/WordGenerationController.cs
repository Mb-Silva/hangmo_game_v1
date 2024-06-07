using Hangmo.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class WordGenerationController : ControllerBase
    {
        private readonly IWordGenerationService _wordGen;

        public WordGenerationController(IWordGenerationService wordGen)
        {
            _wordGen = wordGen;
        }

        [HttpGet(Name = "GetAnswer")]
        public async Task<IActionResult> Get(string prompt)
        {
            var response = await _wordGen.GenerateWordsAsync(prompt);

            return Ok(outputJson);
        }
    }
}
