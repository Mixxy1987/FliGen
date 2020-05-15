using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Queries.LeagueInformation;
using FliGen.Services.Leagues.Application.Queries.LeagueJoinedPlayers;
using FliGen.Services.Leagues.Application.Queries.Leagues;
using FliGen.Services.Leagues.Application.Queries.LeagueSettings;
using FliGen.Services.Leagues.Application.Queries.LeaguesInfo;
using FliGen.Services.Leagues.Application.Queries.LeagueTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Leagues.Application.Queries.PlayerJoinedLeagues;

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
        public Task<IEnumerable<LeagueDto>> GetLeagues([FromQuery]LeaguesQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("information")]
        [Produces(typeof(LeagueInformationDto))]
        public Task<LeagueInformationDto> GetLeagues([FromQuery]LeagueInformationQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("info")]
        [Produces(typeof(LeaguesShortInfoDto))]
        public async Task<LeaguesShortInfoDto> GetAsync([FromQuery]LeaguesShortInfoQuery query)
        {
            return await _mediatr.Send(query);
        }


        [HttpGet("settings")]
        [Produces(typeof(LeagueSettings))]
        public Task<LeagueSettings> GetSettings([FromQuery]LeagueSettingsQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("joinedPlayers")]
        [Produces(typeof(IEnumerable<PlayerInternalIdDto>))]
        public Task<IEnumerable<PlayerInternalIdDto>> GetLeagueJoinedPlayers([FromQuery]LeagueJoinedPlayers query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("playerJoinedLeagues")]
        [Produces(typeof(int[]))]
        public Task<int[]> GetPlayerJoinedLeagues([FromQuery]PlayerJoinedLeagues query)
        {
            return _mediatr.Send(query);
        }
    }
}