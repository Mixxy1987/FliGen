using EventBus.Base.Standard;

namespace FliGen.Application.Events.PlayerRegistered
{
	public class PlayerRegisteredIntegrationEvent : IntegrationEvent
	{
		public string ExternalId { get; }
		public string FirstName { get; }
		public string LastName { get; }

		public PlayerRegisteredIntegrationEvent(
			string externalId,
			string firstName,
			string lastName)
		{
			ExternalId = externalId;
			FirstName = firstName;
			LastName = lastName;
		}
	}
}