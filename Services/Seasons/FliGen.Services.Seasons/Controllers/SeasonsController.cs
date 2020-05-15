using FliGen.Services.Seasons.Application.Dto;
using FliGen.Services.Seasons.Application.Queries.Seasons;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Seasons.Application.Queries.LeaguesIdBySeasonsId;

namespace FliGen.Services.Seasons.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class SeasonsController : ControllerBase
	{
		private readonly ILogger<SeasonsController> _logger;
		private readonly IMediator _mediatr;

		public SeasonsController(ILogger<SeasonsController> logger, IMediator mediatr)
		{
			_logger = logger;
			_mediatr = mediatr;
		}

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Seasons service ready!");
        }

        [HttpGet("league/{id}/seasons")]
        [Produces(typeof(IEnumerable<SeasonDto>))]
        public Task<IEnumerable<SeasonDto>> Get(
            [FromRoute(Name = "id")]int leagueId,
            [FromQuery(Name = "id")]int[] seasonsId,
            [FromQuery]SeasonsQueryType queryType)
        {
            var query = new SeasonsQuery
            {
                LeagueId = leagueId,
                SeasonsId = seasonsId,
                QueryType = queryType
            };

            return _mediatr.Send(query);
        }

        [HttpGet("leaguesIdBySeasonsId")]
        [Produces(typeof(IEnumerable<LeagueIdBySeasonIdDto>))]
        public Task<IEnumerable<LeagueIdBySeasonIdDto>> GetLeaguesIdBySeasonsId(
            [FromQuery(Name = "id")]int[] seasonsId)
        {
            var query = new LeaguesIdBySeasonsIdQuery()
            {
                SeasonsId = seasonsId,
            };

            return _mediatr.Send(query);
        }
    }
}
