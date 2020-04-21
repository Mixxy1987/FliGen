using FliGen.Services.Leagues.Application.Commands.CreateLeague;
using FliGen.Services.Leagues.Application.Commands.DeleteLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings;
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
        public async Task CreateLeagueShouldCreateDbEntity()
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

            league.Should().NotBeNull();
            league.Name.Should().Be(command.Name);
            league.Description.Should().Be(command.Description);
            league.Type.Name.Should().Be(command.LeagueType.Name);
        }

        [Fact]
        public async Task DeleteLeagueShouldDeleteDbEntity()
        {
            //todo:: test cascade deletion!

            var command = new DeleteLeague()
            {
                Id = _testDbFixture.MockedDataInstance.LeagueForDeleteId
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<LeagueDeleted>(
                _testDbFixture.GetLeagueById,
                command.Id);

            await _rabbitMqFixture.PublishAsync(command);

            var league = await creationTask.Task;
            league.Should().BeNull();
        }

        [Fact]
        public async Task UpdateLeagueShouldUpdateDbEntity()
        {
            var command = new UpdateLeague()
            {
                LeagueId = _testDbFixture.MockedDataInstance.LeagueForUpdateId,
                Name = "AAA",
                Description = "BBB",
                LeagueType = new LeagueType() { Name = "None"}
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<LeagueUpdated>(
                _testDbFixture.GetLeagueById,
                command.LeagueId);

            await _rabbitMqFixture.PublishAsync(command);

            var league = await creationTask.Task;

            league.Should().NotBeNull();
            league.Name.Should().Be(command.Name);
            league.Description.Should().Be(command.Description);
            league.Type.Name.Should().Be(command.LeagueType.Name);
        }

        [Fact]
        public async Task UpdateLeagueSettingsShouldUpdateDbEntity()
        {
            var command = new UpdateLeagueSettings()
            {
                LeagueId = _testDbFixture.MockedDataInstance.LeagueForUpdateId,
                TeamsInTour = 50,
                PlayersInTeam = 10,
                RequireConfirmation = true,
                Visibility = false
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<LeagueSettingsUpdated>(
                _testDbFixture.GetLeagueById,
                command.LeagueId);

            await _rabbitMqFixture.PublishAsync(command);

            var league = await creationTask.Task;

            league.Should().NotBeNull();
            league.LeagueSettings.Should().NotBeNull();
            league.LeagueSettings.TeamsInTour.Should().Be(command.TeamsInTour);
            league.LeagueSettings.PlayersInTeam.Should().Be(command.PlayersInTeam);
            league.LeagueSettings.RequireConfirmation.Should().Be(command.RequireConfirmation);
            league.LeagueSettings.Visibility.Should().Be(command.Visibility);
        }
    }
}
