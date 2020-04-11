using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Queries.ToursByPlayerIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TeamsController : ControllerBase
	{
		private readonly ILogger<TeamsController> _logger;
		private readonly IMediator _mediatr;

		public TeamsController(ILogger<TeamsController> logger, IMediator mediatr)
		{
			_logger = logger;
			_mediatr = mediatr;
		}

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Teams service ready!");
        }

		[HttpGet("ToursByPlayerId")]
        [Produces(typeof(ToursByPlayerIdDto))]
        public Task<ToursByPlayerIdDto> Get(int size, int page, [FromQuery]int playerId)
        {
            return _mediatr.Send(new ToursByPlayerIdQuery(size, page, playerId));
        }
	}
}
