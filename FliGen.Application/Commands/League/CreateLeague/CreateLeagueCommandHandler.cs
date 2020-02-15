using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common;

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
            var league = Domain.Entities.League.Create(request.Name, request.Description, Enumeration.FromDisplayName<LeagueType>(request.LeagueType));
            
            await _repository.CreateAsync(league);
            
            return Unit.Value;
        }
    }
}
