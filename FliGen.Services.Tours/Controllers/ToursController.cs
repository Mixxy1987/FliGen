using FliGen.Services.Tours.Application.Commands.TourCancelCommand;
using FliGen.Services.Tours.Application.Commands.TourForwardCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
