using EventBus.Base.Standard;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using System.Threading.Tasks;

namespace FliGen.Application.Events.PlayerRegistered
{
    public class PlayerRegisteredIntegrationEventHandler : IIntegrationEventHandler<PlayerRegisteredIntegrationEvent>
    {
        private readonly IUnitOfWork _uow;

        public PlayerRegisteredIntegrationEventHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(PlayerRegisteredIntegrationEvent @event)
        {
            var playerRepo = _uow.GetRepositoryAsync<Player>();

            Player foundPlayer = await playerRepo.SingleAsync(
                predicate: x => x.FirstName == @event.FirstName &&
                                x.LastName == @event.LastName);

            if (foundPlayer == null)
            {
                await playerRepo.AddAsync(
                    Player.Create(@event.FirstName, @event.LastName, externalId: @event.ExternalId));
            }
            else
            {
                playerRepo.UpdateAsync(
                    Player.GetUpdated(foundPlayer.Id, @event.FirstName, @event.LastName, @event.ExternalId));
            }

            _uow.SaveChanges();
        }
    }
}