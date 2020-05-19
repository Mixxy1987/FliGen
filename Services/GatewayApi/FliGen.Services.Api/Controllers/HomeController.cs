using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Api service ready!");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");
    }
}