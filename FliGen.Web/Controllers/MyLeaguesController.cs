using FliGen.Application.Dto;
using FliGen.Application.Queries.GetMyLeagues;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FliGen.Application.Commands.League.JoinLeague;
using FliGen.Web.Services;

namespace FliGen.Web.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class MyLeaguesController : ControllerBase
	{
		private readonly ILogger<LeaguesController> _logger;
		private readonly IMediator _mediatr;
		private readonly IIdentityService _identityService;

		public MyLeaguesController(
			ILogger<LeaguesController> logger,
			IMediator mediatr,
			IIdentityService identityService)
		{
			_logger = logger;
			_mediatr = mediatr;
			_identityService = identityService;
		}

		[HttpGet]
		[Produces(typeof(IEnumerable<League>))]
		public async Task<IEnumerable<League>> Get()
		{
			var userId = _identityService.GetUserIdentity();

			var leagues = await _mediatr.Send(new GetMyLeaguesQuery(userId));

			return leagues;
		}

		[HttpPost("Join")]
		public async Task Join([FromBody]int id)
		{
			var userId = _identityService.GetUserIdentity();
			JoinLeagueCommand cmd = new JoinLeagueCommand()
			{
				LeagueId = id,
				PlayerExternalId = userId
			};
			await _mediatr.Send(cmd);
		}
	}
}