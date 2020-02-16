using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.League.DeleteLeague
{
    public class DeleteLeagueCommandHandler : IRequestHandler<DeleteLeagueCommand>
    {
        private readonly ILeagueRepository _repository;

        public DeleteLeagueCommandHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteLeagueCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteByIdAsync(request.Id);
            
            return Unit.Value;
        }
    }
}
