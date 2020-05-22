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
        private readonly IPlayersService _playersService;
        private readonly IIdentityService _identityService;

        public ToursController(
            IBusPublisher busPublisher,
            ITracer tracer,
            IToursService toursService,
            IPlayersService playersService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _toursService = toursService;
            _playersService = playersService;
            _identityService = identityService;
        }

        [HttpGet("Ping")]
        public Task HealthCheck()
        {
            return Task.FromResult(_toursService.Ping());
        }

        [HttpGet("id")]
        public async Task<Tour> GetTourById([FromQuery]TourByIdQuery query)
        {
            return await _toursService.GetAsync(query);
        }

        [HttpGet("player/{playerId}/seasons")]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> GetTours(
            [FromRoute]int playerId,
            [FromQuery(Name="id")]int[] seasonIds,
            [FromQuery]ToursQueryType queryType, 
            [FromQuery]int? last)
        {
            return _toursService.GetWithSeasonsAsync(playerId, seasonIds, queryType, last);
        }

        [HttpGet("player/{playerId}")]
        [Produces(typeof(IEnumerable<Tour>))]
        public Task<IEnumerable<Tour>> GetTours([FromRoute]int playerId, [FromQuery]ToursQueryType queryType, [FromQuery]int? last)
        {
            var query = new ToursQuery
            {
                QueryType = queryType,
                Last = last
            };

            return _toursService.GetAsync(playerId, query);
        }

        [HttpGet("player")]
        [Produces(typeof(IEnumerable<Tour>))]
        public async Task<IEnumerable<Tour>> GetMyTours([FromQuery]ToursQueryType queryType, [FromQuery]int? last)
        {
            var playerExternalId = _identityService.GetUserIdentity();
            var internalId = (await _playersService.GetInternalIdAsync(playerExternalId)).InternalId;

            var query = new ToursQuery
            {
                PlayerId = internalId,
                QueryType = queryType,
                Last = last
            };

            return await _toursService.GetAsync(query);
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

        [HttpPost("bulkRegister")]
        public async Task<IActionResult> Register(Messages.Commands.Tours.PlayerRegisterOnTour command)
        {
            if (string.IsNullOrWhiteSpace(command.RegistrationDate))
            {
                command.RegistrationDate = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            }

            if (command.PlayerInternalIds == null ||
                command.PlayerInternalIds.Length == 0)
            {
                command.PlayerExternalId = _identityService.GetUserIdentity();
            }
            
            return await SendAsync(command);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterByTourId(Models.Tours.PlayerRegisterOnTour command)
        {
            var cmd = new Messages.Commands.Tours.PlayerRegisterOnTour()
            {
                TourId = command.TourId,
                LeagueId = command.LeagueId,
                RegistrationDate = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture),
                PlayerExternalId = _identityService.GetUserIdentity()
            };

            return await SendAsync(cmd);
        }
    }
}