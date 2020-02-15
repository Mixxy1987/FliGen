using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.League.DeleteLeague
{
    public class DeleteLeagueCommandHandler : IRequestHandler<DeleteLeagueCommand>
    {
        private readonly IPlayerRepository _repository;

        public DeleteLeagueCommandHandler(IPlayerRepository repository)
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
