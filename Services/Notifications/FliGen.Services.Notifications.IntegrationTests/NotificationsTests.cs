using AutoFixture;
using FliGen.Common.RabbitMq;
using FliGen.Services.Notifications.Application.Commands;
using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Events.TeamsCreated;
using FliGen.Services.Notifications.Application.Events.TourRegistrationOpened;
using FliGen.Services.Notifications.Application.Queries.Leagues;
using FliGen.Services.Notifications.Application.Queries.Players;
using FliGen.Services.Notifications.Application.Queries.Tours;
using FliGen.Services.Notifications.Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace FliGen.Services.Notifications.IntegrationTests
{
    public class NotificationsTests :
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;

        public NotificationsTests(
            WebApplicationFactory<TestStartup> factory)
        {
            _client = factory.WithWebHostBuilder(builder => builder.UseStartup<TestStartup>())
                .CreateClient();
        }

        [Theory]
        [InlineData("notifications/healthcheck")]
        public async Task GivenEndpointsShouldReturnSuccessHttpStatusCode(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task TourRegistrationOpenedShouldCreateInboxNotificationAndSendIt()
        {
            var fix = new Fixture();

            var date = DateTime.UtcNow.AddDays(10);
            int leagueId = fix.Create<int>();
            int tourId = fix.Create<int>();
            var playersData = fix.Create<IList<PlayerInternalIdDto>>();

            var @event = new TourRegistrationOpened(tourId, leagueId, date);

            var leaguesService = Substitute.For<ILeaguesService>();
            var busPublisher = Substitute.For<IBusPublisher>();

            leaguesService.GetLeagueJoinedPlayersAsync(Arg.Any<LeagueJoinedPlayersQuery>())
                .ReturnsForAnyArgs(playersData);

            var commandHandler = new TourRegistrationOpenedHandler(busPublisher, leaguesService);
            await commandHandler.HandleAsync(@event, new CorrelationContext());

            await busPublisher.Received().SendAsync(
                    Arg.Is<InboxNotification>(x => x.PlayerIds.Length == playersData.Count),
                    Arg.Any<CorrelationContext>());
        }

        [Fact]
        public async Task TeamsCreatedShouldCreateInboxNotificationAndSendIt()
        {
            var fix = new Fixture();

            int leagueId = fix.Create<int>();
            int tourId = fix.Create<int>();

            var playersData = new List<PlayerInternalIdDto>()
            {
                new PlayerInternalIdDto{InternalId = 1},
                new PlayerInternalIdDto{InternalId = 2},
                new PlayerInternalIdDto{InternalId = 3},
                new PlayerInternalIdDto{InternalId = 4},
                new PlayerInternalIdDto{InternalId = 5},
                new PlayerInternalIdDto{InternalId = 6},
                new PlayerInternalIdDto{InternalId = 7},
                new PlayerInternalIdDto{InternalId = 8},
                new PlayerInternalIdDto{InternalId = 9},
                new PlayerInternalIdDto{InternalId = 10},
                new PlayerInternalIdDto{InternalId = 11},
                new PlayerInternalIdDto{InternalId = 12},
                new PlayerInternalIdDto{InternalId = 13}
            };

            int[][] teams = 
            {
                new[] {1, 2, 3, 4},
                new[] {5, 6, 7, 8}
            };
            var playersWithRateData = new List<PlayerWithRateDto>();

            foreach (var playerData in playersData)
            {
                playersWithRateData.Add(new PlayerWithRateDto { Id = playerData.InternalId, PlayerLeagueRates = fix.Create<IEnumerable<PlayerLeagueRateDto>>() });
            }

            var @event = new TeamsCreated(teams, tourId, leagueId);

            var playersService = Substitute.For<IPlayersService>();
            var toursService = Substitute.For<IToursService>();
            var busPublisher = Substitute.For<IBusPublisher>();

            playersService.GetAsync(Arg.Any<PlayersQuery>()).ReturnsForAnyArgs(playersWithRateData);
            toursService.GetAsync(Arg.Any<RegisteredOnTourPlayers>())
                .ReturnsForAnyArgs(playersData.Select(x => new PlayerInternalIdDto {InternalId = x.InternalId}));

            var commandHandler = new TeamsCreatedHandler(busPublisher, toursService, playersService);
            await commandHandler.HandleAsync(@event, new CorrelationContext());

            await busPublisher.Received(3).SendAsync(
                Arg.Is<InboxNotification>(
                    x => x.PlayerIds.Length == teams[0].Length || 
                         x.PlayerIds.Length == teams[1].Length ||
                         x.PlayerIds.Length == playersData.Count - teams[0].Length - teams[1].Length),
                Arg.Any<CorrelationContext>());

        }
    }
}
