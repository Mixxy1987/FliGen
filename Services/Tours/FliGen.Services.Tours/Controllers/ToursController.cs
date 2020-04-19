using FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour;
using FliGen.Services.Tours.Application.Commands.TourCancel;
using FliGen.Services.Tours.Application.Commands.TourForward;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.RegisteredOnTourPlayers;
using FliGen.Services.Tours.Application.Queries.TourById;
using FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> Get([FromQuery]ToursByPlayerIdQuery toursByPlayerIdQuery)
        {
            return _mediatr.Send(toursByPlayerIdQuery);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Tour))]
        public Task<Tour> Get(int id)
        {
            return _mediatr.Send(new TourByIdQuery(id));
        }

        [HttpGet("registeredOnTourPlayers")]
        [Produces(typeof(IEnumerable<PlayerInternalIdDto>))]
        public Task<IEnumerable<PlayerInternalIdDto>> Get([FromQuery]RegisteredOnTourPlayers query)
        {
            return _mediatr.Send(query);
        }

        [HttpPost("register")]
        public async Task TourForward([FromBody]PlayerRegisterOnTour cmd)
        {
            await _mediatr.Send(cmd);
        }

		[HttpPost("forward")]
		public async Task TourForward([FromBody]TourForward cmd)
		{
			await _mediatr.Send(cmd);
		}

		[HttpPost("cancel")]
		public async Task TourCancel([FromBody]TourCancel cmd)
		{
			await _mediatr.Send(cmd);
		}
	}
}
