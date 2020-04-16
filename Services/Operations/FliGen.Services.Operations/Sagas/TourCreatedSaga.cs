using System;
using System.Threading.Tasks;
using Chronicle;
using FliGen.Common.RabbitMq;
using FliGen.Services.Operations.Messages.Tours.Events;

namespace FliGen.Services.Operations.Sagas
{
    public class TourCreatedSaga : Saga,
        ISagaStartAction<TourCreated>
    {
        private readonly IBusPublisher _busPublisher;

        public TourCreatedSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourCreated message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(TourCreated message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}