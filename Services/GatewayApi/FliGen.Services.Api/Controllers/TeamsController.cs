using FliGen.Common.RabbitMq;
using FliGen.Services.Api.Messages.Commands.Teams;
using FliGen.Services.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System.Threading.Tasks;

namespace FliGen.Services.Api.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(
            IBusPublisher busPublisher,
            ITracer tracer,
            ITeamsService teamsService,
            IIdentityService identityService) : base(busPublisher, tracer, identityService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("Ping")]
        public Task HealthCheck()
        {
            return Task.FromResult(_teamsService.Ping());
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Post(GenerateTeams command)
        {
            return await SendAsync(command);
        }
    }
}