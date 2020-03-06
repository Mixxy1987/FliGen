using FliGen.Application.Dto;
using FliGen.Application.Queries.GetMyLeagues;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FliGen.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MyLeaguesController : ControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly IMediator _mediatr;

        public MyLeaguesController(ILogger<LeaguesController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<League>))]
        public async Task<IEnumerable<League>> Get()
        {
	        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var leagues = await _mediatr.Send(new GetMyLeaguesQuery(userId));

            return leagues;
        }
    }
}