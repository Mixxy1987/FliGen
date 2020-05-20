using System;
using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Models.Seasons;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Api.Queries.Seasons;

namespace FliGen.Services.Api.Controllers
{
    public class SeasonsController : BaseController
    {
        private readonly ISeasonsService _seasonsService;

        public SeasonsController(
            IBusPublisher busPublisher,
            ITracer tracer,
            ISeasonsService seasonsService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _seasonsService = seasonsService;
        }

        [HttpGet("Ping")]
        public Task HealthCheck()
        {
            return Task.FromResult(_seasonsService.Ping());
        }

        [HttpGet("league/{id}/seasons")]
        [Produces(typeof(IEnumerable<Season>))]
        public async Task<IEnumerable<Season>> Get([FromRoute(Name = "id")]int leagueId, [FromQuery(Name = "id")]int[] seasonsId)
        {
            return await _seasonsService.GetAsync(leagueId, seasonsId);
        }

        [HttpGet("leaguesSeasonsId")]
        [Produces(typeof(IEnumerable<LeaguesSeasonsId>))]
        public Task<IEnumerable<LeaguesSeasonsId>> GetLeaguesSeasonsId(
            [FromQuery(Name = "seasonId")]int[] seasonsId,
            [FromQuery(Name = "leagueId")]int[] leaguesId,
            [FromQuery]LeaguesSeasonsIdQueryType queryType)
        {
            return _seasonsService.GetAsync(leaguesId ?? Array.Empty<int>(), seasonsId ?? Array.Empty<int>(), queryType);
        }
    }
}