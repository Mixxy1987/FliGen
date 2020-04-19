using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Services.Notifications.Application.Builders;
using FliGen.Services.Notifications.Application.Commands;
using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries;
using FliGen.Services.Notifications.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Events.TourRegistrationOpened
{
    public class TourRegistrationOpenedHandler : IEventHandler<TourRegistrationOpened>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILeaguesService _leaguesService;

        public TourRegistrationOpenedHandler(
            IBusPublisher busPublisher, ILeaguesService leaguesService)
        {
            _busPublisher = busPublisher;
            _leaguesService = leaguesService;
        }

        public async Task HandleAsync(TourRegistrationOpened @event, ICorrelationContext context)
        {
            IEnumerable<PlayerInternalIdDto> playersDto = 
                await _leaguesService.GetLeagueJoinedPlayersAsync(new LeagueJoinedPlayersQuery(@event.LeagueId));

            int[] playerIds = playersDto.Select(p => p.InternalId).ToArray();

            InboxNotification notification = InboxNotificationBuilder
                .Create()
                .WithReceiver(playerIds)
                .WithSender("Notification service")
                .WithTopic("Tour registration opened!")
                .WithBody($"Tour number: {@event.TourId}.Tour date: {@event.Date}")
                .Build();

            await _busPublisher.SendAsync(notification, context);
        }
    }
}