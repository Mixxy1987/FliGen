using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Application.Events;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public class UpdateLeagueHandler : ICommandHandler<UpdateLeague>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public UpdateLeagueHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdateLeague command, ICorrelationContext context)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            repo.UpdateAsync(Domain.Entities.League.GetUpdated(
                command.LeagueId,
                command.Name,
                command.Description,
                Enumeration.FromDisplayName<LeagueType>(command.LeagueType.Name)));

            _uow.SaveChanges();

            await _busPublisher.PublishAsync(new LeagueUpdated(command.LeagueId), context);
        }
    }
}
