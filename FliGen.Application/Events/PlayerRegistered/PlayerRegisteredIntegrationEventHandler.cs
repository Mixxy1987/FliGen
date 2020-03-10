using System.Threading.Tasks;
using EventBus.Base.Standard;
using FliGen.Domain.Repositories;

namespace FliGen.Application.Events.PlayerRegistered
{
	public class PlayerRegisteredIntegrationEventHandler : IIntegrationEventHandler<PlayerRegisteredIntegrationEvent>
	{
		private readonly IPlayerRepository _repository;

		public PlayerRegisteredIntegrationEventHandler(IPlayerRepository repository)
		{
			_repository = repository;
		}

		public async Task Handle(PlayerRegisteredIntegrationEvent @event)
		{
			await _repository.UpdateByParametersAsync(Domain.Entities.Player.Create(
				@event.FirstName,
				@event.LastName,
				externalId: @event.ExternalId));
		}
	}
}