using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public class UpdateLeagueHandler : ICommandHandler<UpdateLeague>
    {
        private readonly IUnitOfWork _uow;

        public UpdateLeagueHandler(IUnitOfWork uow)
        {
            _uow = uow;
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
        }
    }
}
