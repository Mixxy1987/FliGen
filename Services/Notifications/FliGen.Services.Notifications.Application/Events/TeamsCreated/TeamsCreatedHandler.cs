using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Services.Notifications.Application.Builders;
using FliGen.Services.Notifications.Application.Commands;
using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries.Players;
using FliGen.Services.Notifications.Application.Queries.Tours;
using FliGen.Services.Notifications.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Events.TeamsCreated
{
    public class TeamsCreatedHandler : IEventHandler<TeamsCreated>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IToursService _toursService;
        private readonly IPlayersService _playersService;

        public TeamsCreatedHandler(
            IBusPublisher busPublisher,
            IToursService toursService,
            IPlayersService playersService)
        {
            _busPublisher = busPublisher;
            _toursService = toursService;
            _playersService = playersService;
        }

        public async Task HandleAsync(TeamsCreated @event, ICorrelationContext context)
        {
            int[] playerIds = @event.Teams.SelectMany(inner => inner).ToArray();

            var query = new PlayersQuery
            {
                LeagueId = new []{ @event.LeagueId },
                PlayerId = playerIds,
                QueryType = PlayersQueryType.Actual,
                Page = 1,
                Size = playerIds.Length
            };

            List<PlayerWithRateDto> playersInfo = (await _playersService.GetAsync(query)).ToList();

            int teamNumber = 1;
            foreach (var team in @event.Teams)
            {
                var body = CreateTeamAndRateBody(team, playersInfo, teamNumber);

                InboxNotification notification = InboxNotificationBuilder
                    .Create()
                    .WithReceiver(team)
                    .WithSender("Notification service")
                    .WithTopic($"Teams for tour: {@event.TourId} in league: {@event.LeagueId} are formed!")
                    .WithBody($"{body}")
                    .Build();

                await _busPublisher.SendAsync(notification, context);
                teamNumber++;
            }

            int[] registeredPlayers =
                (await _toursService.GetAsync(new RegisteredOnTourPlayers { TourId = @event.TourId }))
                .Select(dto => dto.InternalId)
                .ToArray();
            int[] unhappyPlayers = registeredPlayers.Except(playerIds).ToArray();
            
            if (unhappyPlayers.Length != 0)
            {
                InboxNotification notification = InboxNotificationBuilder
                    .Create()
                    .WithReceiver(unhappyPlayers)
                    .WithSender("Notification service")
                    .WithTopic($"Teams for tour: {@event.TourId} in league: {@event.LeagueId} are formed!")
                    .WithBody("Sorry you are out of the game. Try next time!")
                    .Build();

                await _busPublisher.SendAsync(notification, context);
            }
        }

        private string CreateTeamAndRateBody(int[] team, List<PlayerWithRateDto> playersInfo, int teamNumber)
        {
            int probablyBodySize = 200;
            StringBuilder yourTeamBuilder = new StringBuilder(probablyBodySize);
            yourTeamBuilder.Append($"{teamNumber} team is:");
            double teamRate = 0;

            foreach (var playerId in team)
            {
                PlayerWithRateDto playerDto = playersInfo.Single(p => p.Id == playerId);
                var rate = playerDto.PlayerLeagueRates.First().Rate;
                teamRate += rate;
                yourTeamBuilder.Append(
                    $" {playerDto.LastName} {playerDto.FirstName} ({rate.ToString("F1")})");
            }

            yourTeamBuilder.Append($". Team rate: {teamRate.ToString("F1")}");

            return yourTeamBuilder.ToString();
        }
    }
}