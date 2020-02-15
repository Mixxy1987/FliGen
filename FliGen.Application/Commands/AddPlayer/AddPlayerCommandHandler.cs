using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.AddPlayer
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
            var player = Player.Create(request.FirstName, request.LastName, double.Parse(request.Rate));
            
            await _repository.AddAsync(player);
            
            return Unit.Value;
        }
    }
}
