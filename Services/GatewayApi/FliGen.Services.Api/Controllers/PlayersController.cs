using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Players;
using FliGen.Services.Api.Models.Players;
using FliGen.Services.Api.Queries.Players;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("HealthCheck")]
        public Task Get()
        {
            return Task.FromResult(_playersService.HealthCheck());
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeletePlayer command)
        {
            return await SendAsync(command);
        }

        [HttpPost("InboxNotification")]
        public async Task<IActionResult> InboxNotification([FromBody]InboxNotification command)
        {
            return await SendAsync(command);
        }
    }
}