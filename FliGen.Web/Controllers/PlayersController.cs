using FliGen.Application.Commands.AddPlayer;
using FliGen.Application.Dto;
using FliGen.Application.Queries.GetPlayers;
using FliGen.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Application.Commands.DeletePlayer;
using FliGen.Application.Commands.UpdatePlayer;

namespace FliGen.Web.Controllers
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
        public async Task<IEnumerable<PlayerWithRate>> GetPlayers()
        {
            var players = await _mediatr.Send(new GetPlayersQuery());

            return players;
        }

        [HttpPost]
        public async Task AddPlayer([FromBody]Player player)
        { 
            await _mediatr.Send(new AddPlayerCommand()
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                Rate = player.Rate
            });
        }

        [HttpDelete("{id}")]
        public async Task DeletePlayer(int id)
        {
            await _mediatr.Send(new DeletePlayerCommand()
            {
               Id = id
            });
        }

        [HttpPut("{id}")]
        public async Task UpdatePlayer(int id, [FromBody]Player product)
        {
            await _mediatr.Send(new UpdatePlayerCommand()
            {
                Id = id,
                FirstName = product.FirstName,
                LastName = product.LastName,
                Rate = product.Rate
            });
        }
    }
}