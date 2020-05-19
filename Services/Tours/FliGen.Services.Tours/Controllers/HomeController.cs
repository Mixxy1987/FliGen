using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Tours.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Tours service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}