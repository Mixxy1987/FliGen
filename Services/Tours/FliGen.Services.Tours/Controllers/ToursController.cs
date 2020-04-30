using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.RegisteredOnTourPlayers;
using FliGen.Services.Tours.Application.Queries.TourById;
using FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Tours service ready!");
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> GetTours([FromBody]ToursQuery query)
        {
            return _mediatr.Send(query);
        }

        [HttpGet("playerId/{playerId}/seasons")]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> Get([FromRoute]int playerId, [FromQuery]int[] id, [FromQuery]ToursByPlayerIdQueryType queryType)
        {
            var query = new ToursByPlayerIdQuery
            {
                PlayerId = playerId,
                SeasonIds = id,
                QueryType = queryType
            };

            return _mediatr.Send(query);
        }

        [HttpGet("playerId/{playerId}")]
        [Produces(typeof(IEnumerable<TourDto>))]
        public Task<IEnumerable<TourDto>> Get([FromRoute]int playerId, [FromQuery]ToursByPlayerIdQueryType queryType)
        {
            var query = new ToursByPlayerIdQuery
            {
                PlayerId = playerId,
                QueryType = queryType
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
