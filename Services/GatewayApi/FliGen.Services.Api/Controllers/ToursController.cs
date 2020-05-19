using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Tours;
using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Tours;
using FliGen.Services.Api.Queries.Tours;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace FliGen.Services.Api.Controllers
{
    public class ToursController : BaseController
    {
        private readonly IToursService _toursService;

        public ToursController(
            IBusPublisher busPublisher,
            ITracer tracer,
            IToursService toursService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _toursService = toursService;
        }

        [HttpGet("Ping")]
        public Task HealthCheck()
        {
            return Task.FromResult(_toursService.Ping());
        }

        [HttpGet("id")]
        public async Task<Tour> Get([FromQuery]TourByIdQuery query)
        {
            return await _toursService.GetAsync(query);
        }

        [HttpGet("playerId/{playerId}/seasons")]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> Get([FromRoute]int playerId, [FromQuery]int[] id, [FromQuery]int queryType, [FromQuery]int last)
        {
            var query = new ToursQuery
            {
                PlayerId = playerId,
                SeasonIds = id,
                QueryType = queryType,
                Last = last
            };

            return _toursService.GetWithSeasonsAsync(playerId, query);
        }

        [HttpGet("playerId/{playerId}")]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> Get([FromRoute]int playerId, [FromQuery]int queryType, [FromQuery]int last)
        {
            var query = new ToursQuery
            {
                QueryType = queryType,
                Last = last
            };

            return _toursService.GetAsync(playerId, query);
        }

        [HttpGet("registeredOnTourPlayers")]
        public async Task<IEnumerable<PlayerInternalId>> Get([FromQuery]RegisteredOnTourPlayers query)
        {
            return await _toursService.GetAsync(query);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Post(TourCancel command)
        {
            return await SendAsync(command);
        }

        [HttpPost("forward")]
        public async Task<IActionResult> Post(TourForward command)
        {
            return await SendAsync(command);
        }

        [HttpPost("back")]
        public async Task<IActionResult> Post(TourBack command)
        {
            return await SendAsync(command);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(PlayerRegisterOnTour command)
        {
            if (string.IsNullOrWhiteSpace(command.RegistrationDate))
            {
                command.RegistrationDate = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            }
            return await SendAsync(command);
        }
    }
}