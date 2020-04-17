using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Application.Events;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Commands.DeletePlayer
{
    public class DeletePlayerHandler : ICommandHandler<DeletePlayer>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public DeletePlayerHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeletePlayer command, ICorrelationContext context)
        {
            var repo = _uow.GetRepository<Domain.Entities.Player>();
            repo.Delete(command.Id);

            _uow.SaveChanges();
            await _busPublisher.PublishAsync(new PlayerDeleted(), context);
        }
    }
}
