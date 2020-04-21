using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;
using FliGen.Services.Leagues.Application.Events;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    public class DeleteLeagueHandler : ICommandHandler<DeleteLeague>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public DeleteLeagueHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteLeague command, ICorrelationContext context)
        {
            var repo = _uow.GetRepository<Domain.Entities.League>();

            var league = repo.Single(
                predicate: l => l.Id == command.Id,
                include:x => x
                    .Include(l => l.LeagueSettings)
                    .Include(l => l.LeaguePlayerLinks));

            if (league is null)
            {
                return;
            }

            repo.Delete(league.Id);

            _uow.SaveChanges();

            await _busPublisher.PublishAsync(new LeagueDeleted(), context);
        }
    }
}
