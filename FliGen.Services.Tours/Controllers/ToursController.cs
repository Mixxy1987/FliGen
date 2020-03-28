using FliGen.Services.Tours.Application.Commands.TourCancelCommand;
using FliGen.Services.Tours.Application.Commands.TourForwardCommand;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.MyTours;
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

        [HttpGet]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> Get([FromQuery]int userId, int size)
        {
            return _mediatr.Send(new MyToursQuery(userId, size));
        }

		[HttpPost("forward")]
		public async Task TourForward([FromBody]TourForwardCommand cmd)
		{
			await _mediatr.Send(cmd);
		}

		[HttpPost("cancel")]
		public async Task TourCancel([FromBody]TourCancelCommand cmd)
		{
			await _mediatr.Send(cmd);
		}
	}
}
