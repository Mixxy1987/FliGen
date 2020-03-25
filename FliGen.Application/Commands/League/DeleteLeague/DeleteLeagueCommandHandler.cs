using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.League.DeleteLeague
{
    public class DeleteLeagueCommandHandler : IRequestHandler<DeleteLeagueCommand>
    {
        private readonly IUnitOfWork _uow;

        public DeleteLeagueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<Unit> Handle(DeleteLeagueCommand request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepository<Domain.Entities.League>();
            repo.Delete(request.Id);

            _uow.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
