using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common.Repository;

namespace FliGen.Application.Commands.Player.AddPlayer
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand>
    {
        private readonly IUnitOfWork _uow;

        public AddPlayerCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = Domain.Entities.Player.Create(
                request.FirstName,
                request.LastName,
                rate: request.Rate);

            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            await repo.AddAsync(player, cancellationToken);

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
