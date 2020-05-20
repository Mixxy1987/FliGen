using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Leagues.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Leagues service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}