using FliGen.Services.Tours.Application.Commands.TourForward;
using FliGen.Services.Tours.Application.Events;
using FliGen.Services.Tours.Domain.Entities;
using FliGen.Services.Tours.IntegrationTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FliGen.Services.Tours.Domain.Entities.Enum;
using Xunit;

namespace FliGen.Services.Tours.IntegrationTests
{
    public class ToursTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;

        public ToursTests(
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
        [InlineData("tours/healthcheck")]
        public async Task GivenEndpointsShouldReturnSuccessHttpStatusCode(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task TourForwardWithDateShouldCreateDbEntity()
        {
            string date = DateTime.UtcNow.AddDays(100).ToShortDateString();
            int seasonId = 10;

            var command = new TourForward()
            {
                Date = date,
                SeasonId = seasonId
            };

            var creationTask = await _rabbitMqFixture.SubscribeAndGetAsync<TourCreated>(
                _testDbFixture.GetTourByDate,
                date);

            await _rabbitMqFixture.PublishAsync(command);

            Tour tour = await creationTask.Task;

            tour.TourStatus.Should().Be(TourStatus.Planned);
            tour.SeasonId.Should().Be(seasonId);
            tour.GuestCount.Should().BeNull();
            tour.HomeCount.Should().BeNull();
        }
    }
}
