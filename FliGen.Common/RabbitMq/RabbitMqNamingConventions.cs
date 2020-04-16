using FliGen.Common.Extensions;
using FliGen.Common.Messages;
using RawRabbit.Common;
using System;
using System.Reflection;

namespace FliGen.Common.RabbitMq
{
    public class RabbitMqNamingConventions : NamingConventions
    {
        public RabbitMqNamingConventions(string defaultNamespace)
        {
            Init(defaultNamespace);
        }

        private void Init(string defaultNamespace)
        {
            ExchangeNamingConvention = type => GetNamespace(type, defaultNamespace).ToLowerInvariant();
            RoutingKeyConvention = type =>
                $"#.{GetRoutingKeyNamespace(type, defaultNamespace)}{type.Name.Underscore()}".ToLowerInvariant();
            ErrorExchangeNamingConvention = () => $"{defaultNamespace}.error";
            RetryLaterExchangeConvention = span => $"{defaultNamespace}.retry";
            RetryLaterQueueNameConvetion = (exchange, span) =>
                $"{defaultNamespace}.retry_for_{exchange.Replace(".", "_")}_in_{span.TotalMilliseconds}_ms".ToLowerInvariant();
        }

        private static string GetRoutingKeyNamespace(Type type, string defaultNamespace)
        {
            var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

            return string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";
        }

        private static string GetNamespace(Type type, string defaultNamespace)
        {
            var @namespace = type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace;

            return string.IsNullOrWhiteSpace(@namespace) ? "#" : $"{@namespace}";
        }
    }
}