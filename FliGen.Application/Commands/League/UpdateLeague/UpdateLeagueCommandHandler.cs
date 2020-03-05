using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.League.UpdateLeague
{
    public class UpdateLeagueCommandHandler : IRequestHandler<UpdateLeagueCommand>
    {
        private readonly ILeagueRepository _repository;

        public UpdateLeagueCommandHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateLeagueCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(Domain.Entities.League.GetUpdated(
                request.Id,
                request.Name,
                request.Description,
                Enumeration.FromDisplayName<LeagueType>(request.LeagueType.Name)));
            
            return Unit.Value;
        }
    }
}
