using FliGen.Application.Queries;
using FliGen.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Application.Dto;

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
        public async Task<IEnumerable<PlayerWithRate>> GetPlayers()
        {
            var players = await _mediatr.Send(new GetPlayersQuery());

            return players;
        }
    }
}