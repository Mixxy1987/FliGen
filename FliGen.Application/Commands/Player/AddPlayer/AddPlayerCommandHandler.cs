using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.Player.AddPlayer
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand>
    {
        private readonly IPlayerRepository _repository;

        public AddPlayerCommandHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = Domain.Entities.Player.Create(request.FirstName, request.LastName, double.Parse(request.Rate));
            
            await _repository.AddAsync(player);
            
            return Unit.Value;
        }
    }
}
