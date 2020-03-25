using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.Player.DeletePlayer
{
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
    {
        private readonly IUnitOfWork _uow;

        public DeletePlayerCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepository<Domain.Entities.Player>();
            repo.Delete(request.Id);

            _uow.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
