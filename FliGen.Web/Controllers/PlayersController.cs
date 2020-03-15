using FliGen.Application.Dto;
using FliGen.Application.Queries.GetPlayers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Application.Commands.Player.AddPlayer;
using FliGen.Application.Commands.Player.UpdatePlayer;
using FliGen.Application.Commands.Player.DeletePlayer;
using Microsoft.AspNetCore.Authorization;

namespace FliGen.Web.Controllers
{
	[Authorize]
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
        public async Task<IEnumerable<PlayerWithRate>> GetPlayers()
        {
            var players = await _mediatr.Send(new GetPlayersQuery());

            return players;
        }

        [HttpPost]
        public async Task Add([FromBody]AddPlayerCommand cmd)
        { 
            await _mediatr.Send(cmd);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _mediatr.Send(new DeletePlayerCommand()
            {
               Id = id
            });
        }

        [HttpPut]
        public async Task Update([FromBody]UpdatePlayerCommand cmd)
        {


            await _mediatr.Send(cmd);
        }
    }
}