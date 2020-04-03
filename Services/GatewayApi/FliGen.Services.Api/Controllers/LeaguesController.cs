﻿using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Leagues;
using FliGen.Services.Api.Models.Leagues;
using FliGen.Services.Api.Queries.Leagues;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Api.Controllers
{
    public class LeaguesController : BaseController
    {
        private readonly ILeaguesService _leaguesService;
        private readonly IIdentityService _identityService;

        public LeaguesController(
            IBusPublisher busPublisher,
            ITracer tracer,
            ILeaguesService leaguesService,
            IIdentityService identityService) : base(busPublisher, tracer)
        {
            _leaguesService = leaguesService;
            _identityService = identityService;
        }

        [HttpGet("HealthCheck")]
        public Task Get()
        {
            return Task.FromResult(_leaguesService.HealthCheck());
        }

        [HttpGet]
        public async Task<IEnumerable<League>> GetAsync()
        {
            var playerId = _identityService.GetUserIdentity();

            return await _leaguesService.GetAsync(new LeaguesQuery{ PlayerId = playerId });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLeague command)
        {
            return await SendAsync(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await SendAsync(new DeleteLeague(id));
        }


        [HttpPost("Join")]
        public async Task<IActionResult> Join([FromBody]int id)
        {
            var playerId = _identityService.GetUserIdentity();
            
            return await SendAsync(new JoinLeague(id, playerId));
        }
    }
}