using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Extensions;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace SagasTests.Fixtures
{
    public class RabbitMqFixture : IDisposable
    {
        private const string PlayersServiceName = "players";
        private const string ToursServiceName = "tours";
        private readonly RawRabbit.Instantiation.Disposable.BusClient _client;

        public RabbitMqFixture()
        {
            _client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions()
            {
                ClientConfiguration = new RawRabbitConfiguration
                {
                    Hostnames = new List<string> { "localhost" },
                    VirtualHost = "/",
                    Port = 5672,
                    Username = "guest",
                    Password = "guest",
                },
                DependencyInjection = ioc =>
                {
                    ioc.AddSingleton<INamingConventions>(new RabbitMqNamingConventions(PlayersServiceName))
                       .AddSingleton<INamingConventions>(new RabbitMqNamingConventions(ToursServiceName));
                },
                Plugins = p => p
                    .UseAttributeRouting()
                    .UseRetryLater()
                    .UseMessageContext<CorrelationContext>()
                    .UseContextForwarding()
            });
        }

        public Task PublishAsync<TMessage>(TMessage message, string @namespace = null) where TMessage : class
        {
            return _client.PublishAsync(message, ctx =>
                ctx.UseMessageContext(CorrelationContext.Empty)
                    .UsePublishConfiguration(p => p.WithRoutingKey(GetRoutingKey(message, @namespace))));
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private string GetRoutingKey<T>(T message, string @namespace = null)
        {
            @namespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{@namespace}{typeof(T).Name.Underscore()}".ToLowerInvariant();
        }
    }
}