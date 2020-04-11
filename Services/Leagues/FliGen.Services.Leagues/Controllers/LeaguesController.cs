using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Queries.Leagues;
using FliGen.Services.Leagues.Application.Queries.LeagueSettings;
using FliGen.Services.Leagues.Application.Queries.LeagueTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly IMediator _mediatr;

        public LeaguesController(
            ILogger<LeaguesController> logger,
            IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Leagues service ready!");
        }

        [HttpGet("types")]
        [Produces(typeof(IEnumerable<LeagueType>))]
        public Task<IEnumerable<LeagueType>> GetTypes()
        {
            return _mediatr.Send(new LeagueTypesQuery());
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<LeagueDto>))]
        public Task<IEnumerable<LeagueDto>> GetLeagues([FromQuery]LeaguesQuery leaguesQuery)
        {
            return _mediatr.Send(leaguesQuery);
        }

        [HttpGet("settings/{id}")]
        [Produces(typeof(LeagueSettings))]
        public Task<LeagueSettings> GetSettings(int id)
        {
            return _mediatr.Send(new LeagueSettingsQuery(id));
        }
    }
}