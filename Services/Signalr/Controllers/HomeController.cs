using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Signalr.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("FliGen SignalR service");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}