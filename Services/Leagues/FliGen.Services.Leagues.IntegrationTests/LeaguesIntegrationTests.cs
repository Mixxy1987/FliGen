using AutoMapper;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Application.Commands.CreateLeague;
using FliGen.Services.Leagues.Application.Commands.DeleteLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeague;
using FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Events;
using FliGen.Services.Leagues.Application.Queries.Leagues;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.IntegrationTests.Fixtures;
using FliGen.Services.Leagues.Mappings;
using FliGen.Services.Leagues.Persistence.Contexts;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly Mapper _mapper;

        public LeaguesIntegrationTests(
            TestDbFixture testDbFixture,
            RabbitMqFixture rabbitMqFixture,
            WebApplicationFactory<TestStartup> factory)
        {
            _testDbFixture = testDbFixture;
            _rabbitMqFixture = rabbitMqFixture;
            _client = factory.WithWebHostBuilder(builder => builder.UseStartup<TestStartup>())
                .CreateClient();
            _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<LeaguesProfile>(); }));
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
                Id = _testDbFixture.MockedDataInstance.LeagueForUpdateId,
                Name = "AAA",
                Description = "BBB",
                LeagueType = new LeagueType() { Name = "None"}
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<LeagueUpdated>(
                _testDbFixture.GetLeagueById,
                command.Id);

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
            var response = await _client.GetAsync($"{endpoint}?LeagueId={_testDbFixture.MockedDataInstance.LeagueForJoinId1}");

            var data = await response.ReadContentAs<IEnumerable<PlayerInternalIdDto>>();

            data.Count().Should().Be(_testDbFixture.MockedDataInstance.League1JoinedPlayersCount);
        }

        [Theory]
        [InlineData("leagues/joinedPlayers")]
        public async Task LeagueJoinedPlayersQueryShouldThrowExceptionForNonExistingLeague(string endpoint)
        {
            var response = await _client.GetAsync($"{endpoint}?LeagueId={int.MaxValue}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task LeagueQueryWithExternalIdOnlyShouldReturnValidData()
        {
            var command = new LeaguesQuery
            {
                PlayerExternalId = Guid.NewGuid().ToString(),
            };

            var retDto = new PlayerInternalIdDto
            {
                InternalId = _testDbFixture.MockedDataInstance.Player1
            };

            var playersService = Substitute.For<IPlayersService>();
            playersService.GetInternalIdAsync(command.PlayerExternalId).ReturnsForAnyArgs(retDto);

            await using (var context = _testDbFixture.LeaguesContextFactory.Create())
            {
                var uow = new UnitOfWork<LeaguesContext>(context);
                var commandHandler = new LeaguesQueryHandler(uow, _mapper, playersService);
                List<LeagueDto> result = (await commandHandler.Handle(command, CancellationToken.None)).Items.ToList();
                result.Count.Should().Be(context.Leagues.Count());

                List<PlayerWithLeagueStatusDto> playerWithLeagueStatuses = result.SelectMany(l => l.PlayersLeagueStatuses).ToList();

                const int player1ExistsInLeaguesCount = 2;
                playerWithLeagueStatuses.Count.Should().Be(player1ExistsInLeaguesCount);
            }
        }

        [Fact]
        public async Task LeagueQueryWithExternalIdAndLeaguesFilterShouldReturnValidData()
        {
            var command = new LeaguesQuery
            {
                PlayerExternalId = Guid.NewGuid().ToString(),
                LeagueId = new []
                {
                    _testDbFixture.MockedDataInstance.LeagueForJoinId1,
                    _testDbFixture.MockedDataInstance.LeagueForJoinId2,
                }
            };

            var retDto = new PlayerInternalIdDto
            {
                InternalId = _testDbFixture.MockedDataInstance.Player1
            };

            var playersService = Substitute.For<IPlayersService>();
            playersService.GetInternalIdAsync(command.PlayerExternalId).ReturnsForAnyArgs(retDto);

            await using (var context = _testDbFixture.LeaguesContextFactory.Create())
            {
                var uow = new UnitOfWork<LeaguesContext>(context);
                var commandHandler = new LeaguesQueryHandler(uow, _mapper, playersService);
                List<LeagueDto> result = (await commandHandler.Handle(command, CancellationToken.None)).Items.ToList();

                const int expectedLeaguesCount = 2;
                result.Count.Should().Be(expectedLeaguesCount);

                List<PlayerWithLeagueStatusDto> playerWithLeagueStatuses = result.SelectMany(l => l.PlayersLeagueStatuses).ToList();

                const int player1ExistsInLeaguesCount = 1;
                playerWithLeagueStatuses.Count.Should().Be(player1ExistsInLeaguesCount);
            }
        }

        [Fact]
        public async Task LeagueQueryLeaguesFilterShouldReturnValidData()
        {
            var command = new LeaguesQuery
            {
                LeagueId = new[]
                {
                    _testDbFixture.MockedDataInstance.LeagueForJoinId1,
                    _testDbFixture.MockedDataInstance.LeagueForJoinId2,
                }
            };

            var playersService = Substitute.For<IPlayersService>();

            await using (var context = _testDbFixture.LeaguesContextFactory.Create())
            {
                var uow = new UnitOfWork<LeaguesContext>(context);
                var commandHandler = new LeaguesQueryHandler(uow, _mapper, playersService);
                List<LeagueDto> result = (await commandHandler.Handle(command, CancellationToken.None)).Items.ToList();

                const int expectedLeaguesCount = 2;
                result.Count.Should().Be(expectedLeaguesCount);
            }
        }

        [Fact]
        public async Task LeagueQueryPlayerInternalIdsFilterAndLeaguesFilterShouldReturnValidData()
        {
            var command = new LeaguesQuery
            {
                LeagueId = new[]
                {
                    _testDbFixture.MockedDataInstance.LeagueForJoinId1,
                    _testDbFixture.MockedDataInstance.LeagueForJoinId2,
                },
                Pid = new []
                {
                    _testDbFixture.MockedDataInstance.Player1,
                    _testDbFixture.MockedDataInstance.Player3,
                    _testDbFixture.MockedDataInstance.Player5,
                }
            };

            var playersService = Substitute.For<IPlayersService>();

            await using (var context = _testDbFixture.LeaguesContextFactory.Create())
            {
                var uow = new UnitOfWork<LeaguesContext>(context);
                var commandHandler = new LeaguesQueryHandler(uow, _mapper, playersService);
                List<LeagueDto> result = (await commandHandler.Handle(command, CancellationToken.None)).Items.ToList();

                const int expectedLeaguesCount = 2;
                result.Count.Should().Be(expectedLeaguesCount);

                List<PlayerWithLeagueStatusDto> playerWithLeagueStatuses = result.SelectMany(l => l.PlayersLeagueStatuses).ToList();

                const int player1ExistsInLeaguesCount = 4;
                playerWithLeagueStatuses.Count.Should().Be(player1ExistsInLeaguesCount);
            }
        }
    }
}
