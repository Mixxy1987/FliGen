﻿using FliGen.Common.Extensions;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using FliGen.Services.Leagues.Domain.Entities;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.IntegrationTests.Fixtures
{
    public class RabbitMqFixture : IDisposable
    {
        private const string ServiceName = "leagues";

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

        public IBusPublisher GetPublisher()
        {
            return new BusPublisher(_client);
        }

        public Task PublishAsync<TMessage>(TMessage message, string @namespace = null) where TMessage : class
        {
            return _client.PublishAsync(message, ctx =>
                ctx.UseMessageContext(CorrelationContext.Empty)
                    .UsePublishConfiguration(p => p.WithRoutingKey(GetRoutingKey(message, @namespace))));
        }

        public async Task<TaskCompletionSource<League>> SubscribeAndGetAsync<TEvent>(
            Func<int, TaskCompletionSource<League>, Task> onMessageReceived,
            int id) where TEvent : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<League>();
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

        public async Task<TaskCompletionSource<League>> SubscribeAndGetAsync<TEvent>(
            Func<string, TaskCompletionSource<League>, Task> onMessageReceived,
            string name) where TEvent : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<League>();
            var guid = Guid.NewGuid().ToString();

            await _client.SubscribeAsync<TEvent>(
                async _ => await onMessageReceived(name, taskCompletionSource),
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