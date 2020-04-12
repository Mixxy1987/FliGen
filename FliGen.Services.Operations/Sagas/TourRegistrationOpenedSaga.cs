using System;
using System.Threading.Tasks;
using Chronicle;
using FliGen.Common.RabbitMq;
using FliGen.Services.Operations.Messages.Tours.Events;

namespace FliGen.Services.Operations.Sagas
{
    public class TourRegistrationOpenedSaga : Saga,
        ISagaStartAction<TourRegistrationOpened>
    {
        private readonly IBusPublisher _busPublisher;

        public TourRegistrationOpenedSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourRegistrationOpened message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(TourRegistrationOpened message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}