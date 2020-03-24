using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common.Repository;

namespace FliGen.Application.Commands.League.CreateLeague
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
            var league = Domain.Entities.League.Create(
                request.Name,
                request.Description,
                Enumeration.FromDisplayName<LeagueType>(request.LeagueType.Name));

            var repo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            await repo.AddAsync(league, cancellationToken);

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
