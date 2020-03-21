using FliGen.Application.Commands.League.DeleteLeague;
using FliGen.Application.Commands.League.UpdateLeague;
using FliGen.Application.Dto;
using FliGen.Application.Queries.League.LeagueSettings;
using FliGen.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FliGen.Application.Queries.League.LeagueInformation;

namespace FliGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ILogger<LeagueController> _logger;
        private readonly IMediator _mediatr;
        private readonly IIdentityService _identityService;

        public LeagueController(
            ILogger<LeagueController> logger,
            IMediator mediatr,
            IIdentityService identityService)
        {
            _logger = logger;
            _mediatr = mediatr;
            _identityService = identityService;
        }

        [HttpGet("settings/{id}")]
        [Produces(typeof(LeagueSettings))]
        public Task<LeagueSettings> GetSettings(int id)
        {
            return _mediatr.Send(new LeagueSettingsQuery(id));
        }

        [HttpGet("{id}")]
        [Produces(typeof(LeagueInformation))]
        public Task<LeagueInformation> Get(int id)
        {
            return _mediatr.Send(new LeagueInformationQuery(id));
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