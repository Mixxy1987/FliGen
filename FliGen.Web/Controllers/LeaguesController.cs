using System.Collections.Generic;
using FliGen.Application.Commands.League.CreateLeague;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FliGen.Application.Dto;
using FliGen.Application.Queries.GetLeagues;
using FliGen.Application.Queries.GetLeagueTypes;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly IMediator _mediatr;

        public LeaguesController(ILogger<LeaguesController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet("types")]
        [Produces(typeof(IEnumerable<LeagueType>))]
        public async Task<IEnumerable<LeagueType>> GetLeagueTypes()
        {
            var leagueTypes = await _mediatr.Send(new GetLeagueTypesQuery());

            return leagueTypes;
        }
        
        [HttpGet]
        [Produces(typeof(IEnumerable<League>))]
        public async Task<IEnumerable<League>> GetLeagues()
        {
            var leagueTypes = await _mediatr.Send(new GetLeaguesQuery());

            return leagueTypes;
        }

        [HttpPost]
        public async Task Create([FromBody]CreateLeagueCommand cmd)
        { 
            await _mediatr.Send(cmd);
        }
    }
}