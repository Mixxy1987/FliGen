using System.Net.Http;
using AutoMapper;
using FliGen.Services.Tours;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SagasTests.Fixtures;
using Xunit;

namespace SagasTests
{
    public class SagasTests :
        IClassFixture<TestDbFixture>,
        IClassFixture<RabbitMqFixture>,
        IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly TestDbFixture _testDbFixture;
        private readonly RabbitMqFixture _rabbitMqFixture;
        private readonly HttpClient _client;
        private readonly Mapper _mapper;

        public SagasTests(
            TestDbFixture testDbFixture,
            RabbitMqFixture rabbitMqFixture,
            WebApplicationFactory<TestStartup> factory)
        {
            _testDbFixture = testDbFixture;
            _rabbitMqFixture = rabbitMqFixture;
            _client = factory.WithWebHostBuilder(builder => builder.UseStartup<TestStartup>())
                .CreateClient();


        }
    }
}