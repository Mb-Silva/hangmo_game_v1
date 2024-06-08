using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Repository.Models;
using Hangmo.Server.Requests;
using Hangmo.Server.ResponseModels;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hangmo.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IWordService _wordService;
        public GameController(IGameService gameService, IWordService wordService)
        {
            _gameService = gameService;
            _wordService = wordService;
        }

        [HttpPost("ValidateGuess", Name = "ValidateGuess")]
        public async Task<ActionResult<GuessResponse>> ValidateGuessAsync([FromBody] GuessRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GuessResponse response = await _gameService.MakeGuess(request.GameId, request.Letter);            

            return Ok(response);
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
            var game = await _gameService.GetGameById(id);

            if (game == null) { return NotFound(); }

            return Ok(game);
        }

        [HttpPost("Create")]

        public async Task<ActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var game = await _gameService.AddGame(appUserId, request.Theme);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);

        }

        [HttpPatch("{id}/Update")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameRequest request)
        { if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateGame = await _gameService.UpdateGameById(id, request);

            if (updateGame == null) { return NotFound(); }
            return Ok(updateGame);
        }
    }
}
