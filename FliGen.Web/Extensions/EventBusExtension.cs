using System.Collections.Generic;
using EventBus.Base.Standard;
using FliGen.Application.Events;
using FliGen.Application.Events.PlayerRegistered;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Web.Extensions
{
	public static class EventBusExtension
	{
		public static IEnumerable<IIntegrationEventHandler> GetHandlers()
		{
			return new List<IIntegrationEventHandler>
			{
				new ItemCreatedIntegrationEventHandler(),
				//new PlayerRegisteredIntegrationEventHandler()
			};
		}

		public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
		{
			var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

			eventBus.Subscribe<ItemCreatedIntegrationEvent, ItemCreatedIntegrationEventHandler>();
			eventBus.Subscribe<PlayerRegisteredIntegrationEvent, PlayerRegisteredIntegrationEventHandler>();

			return app;
		}
	}
}