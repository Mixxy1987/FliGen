using FliGen.Application.Commands.League.CreateLeague;
using FliGen.Application.Commands.League.DeleteLeague;
using FliGen.Application.Commands.League.UpdateLeague;
using FliGen.Application.Dto;
using FliGen.Application.Queries.GetLeagues;
using FliGen.Application.Queries.GetLeagueTypes;
using FliGen.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly IMediator _mediatr;
        private readonly IIdentityService _identityService;

        public LeaguesController(
            ILogger<LeaguesController> logger,
            IMediator mediatr,
            IIdentityService identityService)
        {
            _logger = logger;
            _mediatr = mediatr;
            _identityService = identityService;
        }

        [HttpGet("types")]
        [Produces(typeof(IEnumerable<LeagueType>))]
        public Task<IEnumerable<LeagueType>> GetTypes()
        {
            return _mediatr.Send(new GetLeagueTypesQuery());
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<League>))]
        public Task<IEnumerable<League>> Get()
        {
            var userId = _identityService.GetUserIdentity();

            return _mediatr.Send(new GetLeaguesQuery(userId));
        }

        [HttpPost]
        public async Task Create([FromBody]CreateLeagueCommand cmd)
        { 
            await _mediatr.Send(cmd);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _mediatr.Send(new DeleteLeagueCommand()
            {
                Id = id
            });
        }

        [HttpPut]
        public async Task Update([FromBody]UpdateLeagueCommand cmd)
        {
            await _mediatr.Send(cmd);
        }
    }
}