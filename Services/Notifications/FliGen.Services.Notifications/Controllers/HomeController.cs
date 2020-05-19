using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Notifications.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Notifications service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}