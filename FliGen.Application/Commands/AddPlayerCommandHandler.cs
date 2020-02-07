using System.Threading;
using System.Threading.Tasks;
using Fligen.Domain.Entities;
using Fligen.Domain.Repositories;
using MediatR;

namespace FliGen.Application.Commands
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand>
    {
        private readonly IFLiGenRepository _repository;

        public AddPlayerCommandHandler(IFLiGenRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddPlayer(new Player()
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            });

            return Unit.Value;
        }
    }
}
