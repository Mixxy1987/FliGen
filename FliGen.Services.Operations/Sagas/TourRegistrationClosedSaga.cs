using Chronicle;
using FliGen.Common.RabbitMq;
using FliGen.Services.Operations.Messages.Tours.Events;
using System.Threading.Tasks;
using FliGen.Services.Operations.Messages.Tours.Commands;

namespace FliGen.Services.Operations.Sagas
{
    public class TourRegistrationClosedSaga : Saga,
        ISagaStartAction<TourRegistrationClosed>
    {
        private readonly IBusPublisher _busPublisher;

        public TourRegistrationClosedSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourRegistrationClosed message, ISagaContext context)
        {
            await _busPublisher.SendAsync(
                new GenerateTeams(
                    message.TourId,
                    message.LeagueId,
                    message.PlayersInTeam,
                    message.TeamsInTour,
                    message.Pid,
                    message.GenerateTeamsStrategy),
                CorrelationContext.Empty);
        }

        public async Task CompensateAsync(TourRegistrationClosed message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(GenerateTeamsRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(GenerateTeamsRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(TeamsCreated message, ISagaContext context)
        {
            Complete();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(TeamsCreated message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}