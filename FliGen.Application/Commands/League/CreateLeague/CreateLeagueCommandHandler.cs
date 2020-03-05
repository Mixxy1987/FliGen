using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.League.CreateLeague
{
    public class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand>
    {
        private readonly ILeagueRepository _repository;

        public CreateLeagueCommandHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            var league = Domain.Entities.League.Create(
                request.Name,
                request.Description,
                Enumeration.FromDisplayName<LeagueType>(request.LeagueType.Name));
            
            await _repository.CreateAsync(league);
            
            return Unit.Value;
        }
    }
}
