using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public class UpdateLeagueCommandHandler : IRequestHandler<UpdateLeagueCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateLeagueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<Unit> Handle(UpdateLeagueCommand request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            repo.UpdateAsync(Domain.Entities.League.GetUpdated(
                request.Id,
                request.Name,
                request.Description,
                Enumeration.FromDisplayName<LeagueType>(request.LeagueType.Name)));

            _uow.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
