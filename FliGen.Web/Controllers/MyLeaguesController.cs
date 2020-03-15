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
using FliGen.Persistence.Contextes;
using Microsoft.AspNetCore.Identity;

namespace FliGen.Web.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class MyLeaguesController : ControllerBase
	{
		private readonly ILogger<LeaguesController> _logger;
		private readonly IMediator _mediatr;
		private readonly UserManager<AppUser> _userManager;

		public MyLeaguesController(ILogger<LeaguesController> logger, IMediator mediatr, UserManager<AppUser> userManager)
		{
			_logger = logger;
			_mediatr = mediatr;
			_userManager = userManager;
		}

		[HttpGet]
		[Produces(typeof(IEnumerable<League>))]
		public async Task<IEnumerable<League>> Get()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			//var user = await _userManager.FindByIdAsync(userId);

			var leagues = await _mediatr.Send(new GetMyLeaguesQuery(userId));

			return leagues;
		}

		[HttpPost("Join")]
		public async Task Join([FromBody]int id)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			JoinLeagueCommand cmd = new JoinLeagueCommand()
			{
				LeagueId = id,
				PlayerExternalId = userId
			};
			await _mediatr.Send(cmd);
		}
	}
}