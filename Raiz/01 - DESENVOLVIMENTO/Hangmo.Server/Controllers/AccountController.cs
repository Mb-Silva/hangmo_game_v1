using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hangmo.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _userService;
        public AccountController(IAppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUser", Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userService.GetAppUser(appUserId);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("UpdateUser", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] AppUser userModel)
        {
            try
            {
                //Por segurança, deve ser adicionado uma validação se o usuário que está sendo atualizado, é o mesmo que está logado.
                var user = await _userService.UpdateAppUser(userModel);

                if (user == false)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
