using System.Collections.Generic;
using System.Linq;
using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Domain.Entities;
using System.Threading.Tasks;
using FliGen.Common.Types;
using FliGen.Services.Tours.Application.Dto.Enum;
using FliGen.Services.Tours.Application.Queries.LeaguesQuery;
using FliGen.Services.Tours.Application.Services;

namespace FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour
{
    public class PlayerRegisterOnTourHandler : ICommandHandler<PlayerRegisterOnTour>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPlayersService _playersService;
        private readonly ILeaguesService _leaguesService;

        public PlayerRegisterOnTourHandler(
            IUnitOfWork uow,
            IPlayersService playersService,
            ILeaguesService leaguesService)
        {
            _uow = uow;
            _playersService = playersService;
            _leaguesService = leaguesService;
        }

        public async Task HandleAsync(PlayerRegisterOnTour command, ICorrelationContext context)
        {
            var tourRepo = _uow.GetRepositoryAsync<Tour>();

            var playerInternalIdDto = await _playersService.GetInternalIdAsync(command.PlayerExternalId);
            if (playerInternalIdDto is null)
            {
                throw new FliGenException("there_is_no_player_with_such_external_id", $"There is no player with external id: {command.PlayerExternalId}");
            }

            await CheckPlayerStatusOrThrowAsync(command);

            _uow.SaveChanges();
        }

        private async Task CheckPlayerStatusOrThrowAsync(PlayerRegisterOnTour command)
        {
            var leagues = await _leaguesService.GetLeagues(
                new LeaguesQuery()
                {
                    PlayerExternalId = command.PlayerExternalId,
                    LeagueId = new[] { command.LeagueId }
                });

            if (leagues is null)
            {
                throw new FliGenException("there_is_no_league_with_id", $"There is no league with id: {command.LeagueId}");
            }

            var leaguesArr = leagues.ToList();
            if (leaguesArr.Count == 0 || leaguesArr[0].PlayerLeagueJoinStatus != PlayerLeagueJoinStatus.Joined)
            {
                throw new FliGenException("player_is_not_a_member_of_this_league", $"Player with id {command.PlayerExternalId} is not a member of league: {command.LeagueId}");
            }
        }
    }
}
