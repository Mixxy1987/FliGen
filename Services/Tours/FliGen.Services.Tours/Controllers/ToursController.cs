using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.RegisteredOnTourPlayers;
using FliGen.Services.Tours.Application.Queries.TourById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Tours.Application.Queries.SeasonStats;
using FliGen.Services.Tours.Application.Queries.Tours;

namespace FliGen.Services.Tours.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class ToursController : ControllerBase
	{
		private readonly ILogger<ToursController> _logger;
		private readonly IMediator _mediatr;

		public ToursController(ILogger<ToursController> logger, IMediator mediatr)
		{
			_logger = logger;
			_mediatr = mediatr;
		}

        [HttpGet]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> GetTours([FromQuery]ToursQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("player/{playerId}/seasons")]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> Get(
            [FromRoute]int playerId,
            [FromQuery(Name = "id")]int[] seasonsId, 
            [FromQuery]ToursQueryType queryType,
            [FromQuery]int? last)
        {
            var query = new ToursQuery
            {
                PlayerId = playerId,
                SeasonsId = seasonsId,
                QueryType = queryType,
                Last = last
            };

            return _mediatr.Send(query);
        }

        [HttpGet("player/{playerId}")]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> Get([FromRoute]int playerId, [FromQuery]ToursQueryType queryType, [FromQuery]int last)
        {
            var query = new ToursQuery
            {
                PlayerId = playerId,
                QueryType = queryType,
                Last = last
            };

            return _mediatr.Send(query);
        }

        [HttpGet("player")]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> GetMyTours([FromQuery]ToursQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("season/{id}/stats")]
        [Produces(typeof(SeasonStatsDto))]
        public Task<SeasonStatsDto> GetSeasonStats([FromRoute(Name = "id")]int seasonId)
        {
            var query = new SeasonStatsQuery()
            {
                SeasonId = seasonId
            };

            return _mediatr.Send(query);
        }

        [HttpGet("id")]
        [Produces(typeof(TourDto))]
        public Task<TourDto> Get([FromQuery]TourByIdQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("registeredOnTourPlayers")]
        [Produces(typeof(IEnumerable<PlayerInternalIdDto>))]
        public Task<IEnumerable<PlayerInternalIdDto>> Get([FromQuery]RegisteredOnTourPlayers query)
        {
            return _mediatr.Send(query);
        }
	}
}
