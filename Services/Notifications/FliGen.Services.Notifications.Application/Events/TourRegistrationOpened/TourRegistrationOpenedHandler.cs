using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Events.TourRegistrationOpened
{
    public class TourRegistrationOpenedHandler : IEventHandler<TourRegistrationOpened>
    {
        public TourRegistrationOpenedHandler()
        {
            
        }

        public Task HandleAsync(TourRegistrationOpened @event, ICorrelationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}