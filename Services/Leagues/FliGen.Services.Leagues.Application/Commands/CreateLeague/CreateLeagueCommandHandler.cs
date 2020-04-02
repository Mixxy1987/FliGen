using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    public class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand>
    {
        private readonly IUnitOfWork _uow;

        public CreateLeagueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = League.Create(
                request.Name,
                request.Description,
                Enumeration.FromDisplayName<LeagueType>(request.LeagueType.Name));

            var repo = _uow.GetRepositoryAsync<League>();

            await repo.AddAsync(league, cancellationToken);

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
