using FliGen.Services.Leagues.Application.Commands.CreateLeague;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Events;
using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FliGen.Services.Leagues.IntegrationTests
{
    public class LeaguesTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;

        public LeaguesTests(
            TestDbFixture testDbFixture,
            RabbitMqFixture rabbitMqFixture,
            WebApplicationFactory<TestStartup> factory)
        {
            _testDbFixture = testDbFixture;
            _rabbitMqFixture = rabbitMqFixture;
            _client = factory.WithWebHostBuilder(builder => builder.UseStartup<TestStartup>())
                .CreateClient();
        }

        [Theory]
        [InlineData("leagues/healthcheck")]
        public async Task GivenEndpointsShouldReturnSuccessHttpStatusCode(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task LeagueCreateShouldCreateDbEntity()
        {
            var command = new CreateLeague
            {
                Name = "Test league name",
                Description = "Test league descr",
                LeagueType = new LeagueType {Name = "Hockey"}
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<LeagueCreated>(
                _testDbFixture.GetLeagueByName,
                command.Name);

            await _rabbitMqFixture.PublishAsync(command);

            League league = await creationTask.Task;

            league.Name.Should().Be(command.Name);
            league.Description.Should().Be(command.Description);
            league.Type.Name.Should().Be(command.LeagueType.Name);
        }
    }
}
