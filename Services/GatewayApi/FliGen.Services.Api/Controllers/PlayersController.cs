using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Tours;
using FliGen.Services.Api.Models.Players;
using FliGen.Services.Api.Queries;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Api.Messages.Commands.Players;

namespace FliGen.Services.Api.Controllers
{
    public class PlayersController : BaseController
    {
        private readonly IPlayersService _playersService;

        public PlayersController(
            IBusPublisher busPublisher,
            ITracer tracer,
            IPlayersService playersService) : base(busPublisher, tracer)
        {
            _playersService = playersService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerWithRate>> GetAsync([FromQuery]PlayersQuery playersQuery)
        {
            return await _playersService.GetAsync(playersQuery);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePlayer command)
        {
            return await SendAsync(command);
        }
    }
}