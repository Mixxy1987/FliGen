using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Extensions;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using FliGen.Services.Tours.Domain.Entities;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace FliGen.Services.Tours.IntegrationTests.Fixtures
{
    public class RabbitMqFixture : IDisposable
    {
        private const string ServiceName = "tours";

        private readonly RawRabbit.Instantiation.Disposable.BusClient _client;

        public RabbitMqFixture()
        {
            _client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions()
            {
                ClientConfiguration = new RawRabbitConfiguration
                {
                    Hostnames = new List<string> { "localhost" }, // localhost
                    VirtualHost = "/",
                    Port = 5672,
                    Username = "guest",
                    Password = "guest",
                },
                DependencyInjection = ioc =>
                {
                    ioc.AddSingleton<INamingConventions>(new RabbitMqNamingConventions(ServiceName));
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

        public async Task<TaskCompletionSource<bool>> SubscribeAndGetAsync<TEvent>(
            Func<int, TaskCompletionSource<bool>, Task> onMessageReceived,
            int id) where TEvent : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var guid = Guid.NewGuid().ToString();

            await _client.SubscribeAsync<TEvent>(
                async _ => await onMessageReceived(id, taskCompletionSource),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(
                            builder => builder
                                .WithDurability(false)
                                .WithName(guid))));
            return taskCompletionSource;
        }

        public async Task<TaskCompletionSource<Tour>> SubscribeAndGetAsync<TEvent>(
            Func<string, TaskCompletionSource<Tour>, Task> onMessageReceived,
            string date) where TEvent : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<Tour>();
            var guid = Guid.NewGuid().ToString();

            await _client.SubscribeAsync<TEvent>(
                async _ => await onMessageReceived(date, taskCompletionSource),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(
                        builder => builder
                            .WithDurability(false)
                            .WithName(guid))));
            return taskCompletionSource;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private string GetRoutingKey<T>(T message, string @namespace = null)
        {
            @namespace ??= ServiceName;
            @namespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{@namespace}{typeof(T).Name.Underscore()}".ToLowerInvariant();
        }
    }
}