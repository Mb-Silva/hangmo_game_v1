using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Repository.Models;
using Hangmo.Server.Requests;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IWordService _wordService;
        public GameController(IGameService gameService, IWordService wordService)
        {
            _gameService = gameService;
            _wordService = wordService;
        }

        [HttpGet("{id}/ValidateGuess", Name = "ValidateGuess")]
        public ActionResult<List<object>> ValidateGuess(int id, char letra)
        {   
            (bool success, List<(int, char)> positions) = _gameService.FindLetter(id, letra);

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
                int positions = _wordService.GetDailyWord();

                return Ok(positions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            var game =  await _gameService.GetGameById(id);
            
            if (game == null) { return NotFound();}
            
            return Ok(game);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateGame(int appUserId, int wordId) 
        {
            var game = await _gameService.AddGame(appUserId, wordId);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);

        }

        [HttpPatch("{id}/Update")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameUpdateRequest request) 
        {   if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateGame = await _gameService.UpdateGameById(id, request);

            if (updateGame == null) { return NotFound(); }
            return Ok(updateGame);
        }

    }
}
