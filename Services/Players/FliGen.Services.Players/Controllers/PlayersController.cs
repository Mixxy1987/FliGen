using FliGen.Services.Players.Application.Dto;
using FliGen.Services.Players.Application.Queries.Players;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;

namespace FliGen.Services.Players.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMediator _mediatr;

        public PlayersController(ILogger<PlayersController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<PlayerWithRate>))]
        public async Task<IEnumerable<PlayerWithRate>> GetPlayers(int size, int queryType, [FromQuery]int[] leagueId, [FromQuery]int[] playerId)
        {
            return await _mediatr.Send(new PlayersQuery(size, (PlayersQueryType)queryType, leagueId, playerId));
        }

        [HttpPut]
        public async Task Update([FromBody]UpdatePlayerCommand cmd)
        {
            await _mediatr.Send(cmd);
        }
    }
}