using Hangmo.Server.Repository.Models;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IWordsService _wordsService;
        public GameController(IGameService gameService, IWordsService wordsService)
        {
            _gameService = gameService;
            _wordsService = wordsService;
        }

        [HttpGet("ValidatePosition", Name = "ValidatePosition")]
        public ActionResult<List<object>> GetValidatePosition(string palavra, char letra)
        {
            (bool success, List<(int, char)> positions) = _gameService.FindLetter(palavra, letra);

            if (success)
            {
                List<object> serializableList = new List<object>();
                foreach (var item in positions)
                {
                    serializableList.Add(new { Position = item.Item1, Character = item.Item2 });
                }
                return Ok(serializableList);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("DailyWord", Name = "DailyWord")]
        public ActionResult<int> GetDailyWord()
        {
            try
            {
                int positions = _wordsService.GetDailyWord();

                return Ok(positions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
