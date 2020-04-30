using FliGen.Services.Players.Application.Dto;
using FliGen.Services.Players.Application.Queries.PlayerInternalId;
using FliGen.Services.Players.Application.Queries.Players;
using FliGen.Services.Players.Application.Queries.PlayersInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Players service ready!");
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<PlayerWithRate>))]
        public async Task<IEnumerable<PlayerWithRate>> GetAsync([FromQuery]PlayersQuery query)
        {
            return await _mediatr.Send(query);
        }

        [HttpGet("info")]
        [Produces(typeof(PlayersInfoDto))]
        public async Task<PlayersInfoDto> GetAsync([FromQuery]PlayersInfoQuery query)
        {
            return await _mediatr.Send(query);
        }

        [HttpGet("id")]
        [Produces(typeof(PlayerInternalIdDto))]
        public async Task<PlayerInternalIdDto> GetInternalIdAsync([FromQuery]string externalId)
        {
            return await _mediatr.Send(new PlayerInternalIdQuery(externalId));
        }
    }
}