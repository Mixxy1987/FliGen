using System;
using FliGen.Common.Messages;
using FliGen.Common.Types;

namespace FliGen.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, FliGenException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, FliGenException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
