using Hangmo.Repository.Data.DAO;
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
    [Route("[controller]")]
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

        [HttpPost("MakeGuess", Name = "MakeGuess")]
        public async Task<ActionResult<MakeGuessResponse>> MakeGuessAsync([FromBody] MakeGuessRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MakeGuessResponse response = await _gameService.MakeGuess(request.GameId, request.Letter);            

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
        public async Task<ActionResult<GetGameResponse>> GetGameById(int id)
        {
            try
            {
                var response = await _gameService.GetGameById(id);
                return Ok(response);
            }
            catch (KeyNotFoundException) {

                return NotFound();

            }

        }

        [HttpPost("Create")]

        public async Task<ActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _gameService.AddGame(appUserId, request.Theme);
            return CreatedAtAction(nameof(GetGameById), new { Id = response.GameId }, response);

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
