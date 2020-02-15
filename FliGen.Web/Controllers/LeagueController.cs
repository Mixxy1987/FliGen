using System.Collections.Generic;
using FliGen.Application.Commands.League.CreateLeague;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FliGen.Application.Dto;
using FliGen.Application.Queries.GetLeagueTypes;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMediator _mediatr;

        public LeagueController(ILogger<PlayersController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet("leagueTypes")]
        [Produces(typeof(IEnumerable<LeagueType>))]
        public async Task<IEnumerable<LeagueType>> GetLeagueTypes()
        {
            var leagueTypes = await _mediatr.Send(new GetLeagueTypesQuery());

            return leagueTypes;
        }

        [HttpPost]
        public async Task Create([FromBody]CreateLeagueCommand cmd)
        { 
            await _mediatr.Send(cmd);
        }
    }
}