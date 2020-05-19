using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Players.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Players service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}