using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Operations.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Operations Service ready!");
    }
}