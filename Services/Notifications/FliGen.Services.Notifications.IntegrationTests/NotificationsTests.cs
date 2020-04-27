using FliGen.Services.Notifications.Application.Events.TourRegistrationOpened;
using FliGen.Services.Notifications.Application.Services;
using FliGen.Services.Notifications.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FliGen.Common.RabbitMq;
using FliGen.Services.Notifications.Application.Commands;
using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries.Leagues;
using Xunit;
using NSubstitute;


namespace FliGen.Services.Notifications.IntegrationTests
{
    public class NotificationsTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;

        public NotificationsTests(
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

            var command = new TourRegistrationOpened(tourId, leagueId, date);

            var leaguesService = Substitute.For<ILeaguesService>();
            var busPublisher = Substitute.For<IBusPublisher>();

            leaguesService.GetLeagueJoinedPlayersAsync(new LeagueJoinedPlayersQuery(leagueId))
                .ReturnsForAnyArgs(playersData);

            var commandHandler = new TourRegistrationOpenedHandler(busPublisher, leaguesService);
            await commandHandler.HandleAsync(command, new CorrelationContext());

            await busPublisher.Received().SendAsync(
                    Arg.Is<InboxNotification>(x => x.PlayerIds.Length == playersData.Count()),
                    Arg.Any<CorrelationContext>());
        }

        [Fact]
        public async Task TourRegistrationClosedShouldCreateInboxNotificationAndSendIt()
        {
            var fix = new Fixture();

        }
    }
}
