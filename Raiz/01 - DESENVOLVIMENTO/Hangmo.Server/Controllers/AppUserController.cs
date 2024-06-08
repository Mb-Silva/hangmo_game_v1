using Hangmo.Repository.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangmo.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AppUserController : ControllerBase
    {

    }
}
