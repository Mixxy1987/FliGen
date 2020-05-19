using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Seasons.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Seasons service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}