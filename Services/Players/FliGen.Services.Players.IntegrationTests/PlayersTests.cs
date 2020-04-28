using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Application.Commands.AddPlayer;
using FliGen.Services.Players.Application.Commands.DeletePlayer;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;
using FliGen.Services.Players.Application.Events;
using FliGen.Services.Players.Application.Queries.PlayerInternalId;
using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.IntegrationTests.Fixtures;
using FliGen.Services.Players.Mappings;
using FliGen.Services.Players.Persistence.Contexts;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Players.Application.Queries.Players;
using NSubstitute;
using Xunit;

namespace FliGen.Services.Players.IntegrationTests
{
    public class PlayersTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;
        private readonly Mapper _mapper;

        public PlayersTests(
            TestDbFixture testDbFixture,
            RabbitMqFixture rabbitMqFixture,
            WebApplicationFactory<TestStartup> factory)
        {
            _testDbFixture = testDbFixture;
            _rabbitMqFixture = rabbitMqFixture;
            _client = factory.WithWebHostBuilder(builder => builder.UseStartup<TestStartup>())
                .CreateClient();
            _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<PlayersProfile>(); }));
        }

        [Theory]
        [InlineData("players/healthcheck")]
        public async Task GivenEndpointsShouldReturnSuccessHttpStatusCode(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task AddPlayerShouldCreateDbEntity()
        {
            var externalId = Guid.NewGuid().ToString();
            var command = new AddPlayer()
            {
                ExternalId = externalId,
                FirstName = "Test FirstName",
                LastName = "Test LastName"
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<PlayerAdded>(
                _testDbFixture.GetPlayerByExternalId,
                externalId);

            await _rabbitMqFixture.PublishAsync(command);

            Player player = await creationTask.Task;

            player.FirstName.Should().Be(command.FirstName);
            player.LastName.Should().Be(command.LastName);
            player.ExternalId.Should().Be(command.ExternalId);
        }

        [Fact]
        public async Task DeletePlayerShouldDeleteDbEntity()
        {
            var command = new DeletePlayer()
            {
                Id = _testDbFixture.MockedDataInstance.PlayerInternalIdForDelete
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<PlayerDeleted>(
                _testDbFixture.CheckIfPlayerExists,
                _testDbFixture.MockedDataInstance.PlayerInternalIdForDelete);

            await _rabbitMqFixture.PublishAsync(command);

            bool result = await creationTask.Task;

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdatePlayerShouldUpdateDbEntity()
        {
            var command = new UpdatePlayer()
            {
                Id = _testDbFixture.MockedDataInstance.PlayerInternalIdForUpdate,
                FirstName = "Updated firstname",
                LastName = "Updated lastname",
                LeagueId = 1,
                Rate = "8.0"
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<PlayerUpdated>(
                _testDbFixture.GetPlayerAndRateByExternalId,
                _testDbFixture.MockedDataInstance.PlayerExternalIdForUpdate);

            await _rabbitMqFixture.PublishAsync(command);

            (Player player, PlayerRate playerRate) = await creationTask.Task;

            player.FirstName.Should().Be(command.FirstName);
            player.LastName.Should().Be(command.LastName);

            playerRate.Value.Should().Be(double.Parse(command.Rate));
        }

        [Fact]
        public async Task PlayerInternalIdQueryShouldReturnValidData()
        {
            await using (var context = _testDbFixture.PlayersContextFactory.Create())
            {
                var uow = new UnitOfWork<PlayersContext>(context);
                var handler = new PlayerInternalIdQueryHandler(uow, _mapper);
                var dto = await handler.Handle(
                    new PlayerInternalIdQuery(_testDbFixture.MockedDataInstance.ExistingPlayer),
                    CancellationToken.None);

                dto.InternalId.Should().Be(_testDbFixture.MockedDataInstance.ExistingPlayerInternalId);

                (await handler.Handle(
                    new PlayerInternalIdQuery(_testDbFixture.MockedDataInstance.NotExistingPlayer),
                    CancellationToken.None)).Should().BeNull();
            }
        }

        [Fact]
        public async Task PlayersQueryAllTypeShouldReturnData()
        {
            await using (var context = _testDbFixture.PlayersContextFactory.Create())
            {
                var uow = new UnitOfWork<PlayersContext>(context);

                var query = new PlayersQuery
                {
                    Page = 0,
                    Size = 10,
                    QueryType = PlayersQueryType.All
                };

                var handler = new PlayersQueryHandler(uow, _mapper);
                var result = (await handler.Handle(query, CancellationToken.None)).ToList();

                result.Count.Should().Be(query.Size);
            }
        }

        [Fact]
        public async Task PlayersQueryActualTypeShouldReturnData()
        {
            await using (var context = _testDbFixture.PlayersContextFactory.Create())
            {
                var uow = new UnitOfWork<PlayersContext>(context);

                var query = new PlayersQuery
                {
                    Page = 0,
                    Size = 10,
                    QueryType = PlayersQueryType.Actual
                };

                var handler = new PlayersQueryHandler(uow, _mapper);
                var result = (await handler.Handle(query, CancellationToken.None)).ToList();

                result.Count.Should().Be(query.Size);
            }
        }

        [Fact]
        public async Task PlayersQueryForConcretePlayersShouldReturnValidData()
        {
            await using (var context = _testDbFixture.PlayersContextFactory.Create())
            {
                var uow = new UnitOfWork<PlayersContext>(context);

                var query = new PlayersQuery
                {
                    Page = 0,
                    Size = 10,
                    QueryType = PlayersQueryType.Actual,
                    PlayerId = new []
                    {
                        _testDbFixture.MockedDataInstance.PlayerForFilter1,
                        _testDbFixture.MockedDataInstance.PlayerForFilter2,
                    }
                };

                var handler = new PlayersQueryHandler(uow, _mapper);
                var result = (await handler.Handle(query, CancellationToken.None)).ToList();

                result.Count.Should().Be(query.PlayerId.Length);
                result[0].PlayerLeagueRates.Count().Should().Be(2);
                result[1].PlayerLeagueRates.Count().Should().Be(2);
            }
        }

        [Fact]
        public async Task PlayersQueryForConcretePlayersAndConcreteLeaguesShouldReturnValidData()
        {
            await using (var context = _testDbFixture.PlayersContextFactory.Create())
            {
                var uow = new UnitOfWork<PlayersContext>(context);

                var query = new PlayersQuery
                {
                    Page = 0,
                    Size = 10,
                    QueryType = PlayersQueryType.Actual,
                    PlayerId = new[]
                    {
                        _testDbFixture.MockedDataInstance.PlayerForFilter1,
                        _testDbFixture.MockedDataInstance.PlayerForFilter2,
                    },
                    LeagueId = new []
                    {
                        _testDbFixture.MockedDataInstance.LeagueForFilter1
                    }
                };

                var handler = new PlayersQueryHandler(uow, _mapper);
                var result = (await handler.Handle(query, CancellationToken.None)).ToList();

                result.Count.Should().Be(query.PlayerId.Length);
                result[0].PlayerLeagueRates.Count().Should().Be(1);
                result[1].PlayerLeagueRates.Count().Should().Be(1);
            }
        }
    }
}
