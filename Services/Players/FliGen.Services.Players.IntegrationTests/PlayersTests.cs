﻿using FliGen.Services.Players.Application.Commands.AddPlayer;
using FliGen.Services.Players.Application.Commands.DeletePlayer;
using FliGen.Services.Players.Application.Commands.UpdatePlayer;
using FliGen.Services.Players.Application.Events;
using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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

        public PlayersTests(
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
    }
}