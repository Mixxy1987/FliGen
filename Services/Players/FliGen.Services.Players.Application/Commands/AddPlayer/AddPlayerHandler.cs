using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;
using FliGen.Services.Players.Application.Events;

namespace FliGen.Services.Players.Application.Commands.AddPlayer
{
    public class AddPlayerHandler : ICommandHandler<AddPlayer>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public AddPlayerHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(AddPlayer command, ICorrelationContext context)
        {
            var player = Domain.Entities.Player.Create(
                command.FirstName,
                command.LastName,
                externalId: command.ExternalId);

            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            await repo.AddAsync(player);

            _uow.SaveChanges();

            await _busPublisher.PublishAsync(new PlayerAdded(player.Id), context);
        }
    }
}
