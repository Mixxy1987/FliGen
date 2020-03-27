using System.Threading.Tasks;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;

namespace FliGen.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}