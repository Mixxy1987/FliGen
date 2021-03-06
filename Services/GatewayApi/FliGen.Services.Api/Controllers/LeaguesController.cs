﻿using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Leagues;
using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Leagues;
using FliGen.Services.Api.Queries.Leagues;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Types;

namespace FliGen.Services.Api.Controllers
{
    //[Authorize]
    public class LeaguesController : BaseController
    {
        private readonly ILeaguesService _leaguesService;
        private readonly IIdentityService _identityService;

        public LeaguesController(
            IBusPublisher busPublisher,
            ITracer tracer,
            ILeaguesService leaguesService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _leaguesService = leaguesService;
            _identityService = identityService;
        }

        [HttpGet("Ping")]
        public Task HealthCheck()
        {
            return Task.FromResult(_leaguesService.Ping());
        }

        [HttpGet]
        public async Task<PagedResult<League>> GetLeagues(
            [FromQuery(Name = "id")]int[] leaguesId,
            [FromQuery]int? size,
            [FromQuery]int? page)
        {
            var query = new LeaguesQuery(
                _identityService.GetUserIdentity(),
                leaguesId,
                Array.Empty<int>(),
                size,
                page);

            return await _leaguesService.GetAsync(query);
        }

        [HttpGet("my")]
        public async Task<PagedResult<League>> GetMyLeagues(
            [FromQuery(Name = "id")]int[] leaguesId,
            [FromQuery]int? size,
            [FromQuery]int? page)
        {
            var query = new LeaguesQuery(
                _identityService.GetUserIdentity(),
                leaguesId ?? Array.Empty<int>(),
                Array.Empty<int>(),
                size,
                page);
            return await _leaguesService.GetAsync(query);
        }

        [HttpGet("info")]
        public async Task<LeaguesShortInfo> GetLeaguesShortInfo([FromQuery]LeaguesShortInfoQuery query)
        {
            return await _leaguesService.GetLeaguesShortInfoAsync(query);
        }

        [HttpGet("types")]
        public async Task<IEnumerable<League>> GetLeagueTypes()
        {
            return await _leaguesService.GetLeagueTypesAsync(new LeagueTypesQuery());
        }

        [HttpGet("{id}")]
        [Produces(typeof(LeagueInformation))]
        public async Task<LeagueInformation> GetLeagues(int id)
        {
            return await _leaguesService.GetLeagueInformationAsync(new LeagueInformationQuery { Id = id });
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


        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody]int id)
        {
            var playerId = _identityService.GetUserIdentity();

            return await SendAsync(new JoinLeague(id, playerId));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLeague(UpdateLeague command)
        {
            return await SendAsync(command);
        }

        [HttpPut("updateSettings")]
        public async Task<IActionResult> UpdateLeagueSettings(UpdateLeagueSettings command)
        {
            return await SendAsync(command);
        }

        [HttpGet("{id}/settings")]
        [Produces(typeof(LeagueSettings))]
        public async Task<LeagueSettings> GetSettings([FromRoute]int id)
        {
            return await _leaguesService.GetLeagueSettingsAsync(new LeagueSettingsQuery()
            {
                LeagueId = id
            });
        }

        [HttpGet("joinedPlayers")]
        [Produces(typeof(IEnumerable<PlayerInternalId>))]
        public async Task<IEnumerable<PlayerInternalId>> GetLeagueJoinedPlayers([FromQuery]LeagueJoinedPlayersQuery query)
        {
            return await _leaguesService.GetLeagueJoinedPlayersAsync(query);
        }
    }
}