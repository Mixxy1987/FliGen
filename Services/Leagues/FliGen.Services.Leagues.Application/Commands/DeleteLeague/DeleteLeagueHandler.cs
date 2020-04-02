using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    public class DeleteLeagueHandler : ICommandHandler<DeleteLeague>
    {
        private readonly IUnitOfWork _uow;

        public DeleteLeagueHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task HandleAsync(DeleteLeague command, ICorrelationContext context)
        {
            var repo = _uow.GetRepository<Domain.Entities.League>();

            var league = repo.Single(
                predicate: l => l.Id == command.Id,
                include:x => x
                    .Include(l => l.LeagueSettings)
                    .Include(l => l.LeaguePlayerLinks));

            if (league is null)
            {
                return null;
            }

            repo.Delete(league);
            //repo.Delete(command.Id);

            _uow.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
