using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using FliGen.Common.Extensions;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Queries.LeagueSettings;
using FliGen.Services.Leagues.Domain.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace FliGen.Services.Leagues.IntegrationTests
{
    public class LeaguesIntegrationTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;

        public LeaguesIntegrationTests(
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
                TeamsInTour = _testDbFixture.MockedDataInstance.TeamsInTour,
                PlayersInTeam = _testDbFixture.MockedDataInstance.PlayersInTeam,
                RequireConfirmation = true,
                Visibility = true
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
            league.IsRequireConfirmation().Should().Be(command.RequireConfirmation);
            league.IsVisible().Should().Be(command.Visibility);
        }

        [Theory]
        [InlineData("leagues/settings")]
        public async Task LeagueSettingsQueryShouldReturnSettings(string endpoint)
        {
            var response = await _client.GetAsync($"{endpoint}?LeagueId={_testDbFixture.MockedDataInstance.LeagueForUpdateId}");
            response.IsSuccessStatusCode.Should().BeTrue();

            var settings = await response.ReadContentAs<Application.Dto.LeagueSettings>();

            settings.TeamsInTour.Should().Be(_testDbFixture.MockedDataInstance.TeamsInTour);
            settings.PlayersInTeam.Should().Be(_testDbFixture.MockedDataInstance.PlayersInTeam);
        }

        [Theory]
        [InlineData("leagues/types")]
        public async Task LeagueTypeQueryShouldReturnTypes(string endpoint)
        {
            var response = await _client.GetAsync($"{endpoint}");

            var types = await response.ReadContentAs<IEnumerable<LeagueType>>();

            types.Count().Should().BePositive();
        }

        [Theory]
        [InlineData("leagues/joinedPlayers")]
        public async Task LeagueJoinedPlayersQueryShouldReturnValidCount(string endpoint)
        {
            var response = await _client.GetAsync($"{endpoint}?LeagueId={_testDbFixture.MockedDataInstance.LeagueForJoinId}");

            var data = await response.ReadContentAs<IEnumerable<PlayerInternalIdDto>>();

            data.Count().Should().Be(_testDbFixture.MockedDataInstance.JoinedPlayersCount);
        }

        [Theory]
        [InlineData("leagues/joinedPlayers")]
        public async Task LeagueJoinedPlayersQueryShouldThrowExceptionForNonExistingLeague(string endpoint)
        {
            var response = await _client.GetAsync($"{endpoint}?LeagueId={int.MaxValue}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
