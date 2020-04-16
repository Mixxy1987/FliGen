using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Commands.DeletePlayer
{
    public class DeletePlayerHandler : ICommandHandler<DeletePlayer>
    {
        private readonly IUnitOfWork _uow;

        public DeletePlayerHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task HandleAsync(DeletePlayer command, ICorrelationContext context)
        {
            var repo = _uow.GetRepository<Domain.Entities.Player>();
            repo.Delete(command.Id);

            _uow.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
