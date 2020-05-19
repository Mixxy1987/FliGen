using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Teams.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Teams service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}