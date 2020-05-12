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
using FliGen.Services.Api.Models.Seasons;

namespace FliGen.Services.Api.Controllers
{
    public class SeasonsController : BaseController
    {
        private readonly ISeasonsService _seasonsService;
        private readonly IIdentityService _identityService;

        public SeasonsController(
            IBusPublisher busPublisher,
            ITracer tracer,
            ISeasonsService seasonsService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _seasonsService = seasonsService;
            _identityService = identityService;
        }

        [HttpGet("league/{id}/seasons")]
        [Produces(typeof(IEnumerable<Season>))]
        public async Task<IEnumerable<Season>> Get([FromRoute(Name = "id")]int leagueId, [FromQuery(Name = "id")]int[] seasonsId)
        {
            return await _seasonsService.GetAsync(leagueId, seasonsId);
        }
    }
}