using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Entities;
using System.Threading.Tasks;
using FliGen.Services.Leagues.Application.Common;

namespace FliGen.Services.Leagues.Application.Commands.JoinLeague
{
    public class JoinLeagueHandler : ICommandHandler<JoinLeague>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPlayersService _playersService;

        public JoinLeagueHandler(
            IUnitOfWork uow,
            IPlayersService playersService)
        {
            _uow = uow;
            _playersService = playersService;
        }

        public async Task HandleAsync(JoinLeague command, ICorrelationContext context)
        {
            PlayerInternalIdDto playerIdDto = await _playersService.GetInternalIdAsync(command.PlayerExternalId);
            if (playerIdDto is null)
            {
                throw new FliGenException(ErrorCodes.NoPlayerWithSuchExternalId, $"There is no player with external id: {command.PlayerExternalId}");
            }

            int playerId = playerIdDto.InternalId;

            var leagueSettingsRepo = _uow.GetRepositoryAsync<Domain.Entities.LeagueSettings>();

            Domain.Entities.LeagueSettings leagueSettings =
                await leagueSettingsRepo.SingleAsync(x => x.LeagueId == command.LeagueId);

            if (leagueSettings is null)
            {
                throw new FliGenException(ErrorCodes.NoLeagueSettings, $"There is no league settings for league: {command.LeagueId}");
            }

            var lplinksRepo = _uow.GetRepositoryAsync<LeaguePlayerLink>();

            LeaguePlayerLink lastLink = await 
                lplinksRepo.SingleAsync(x => 
                    x.PlayerId == playerId &&
                    x.LeagueId == command.LeagueId &&
                    x.Actual);

            if (lastLink is null || lastLink.InLeftStatus())
            {
	            LeaguePlayerLink link = leagueSettings.RequireConfirmation
		            ? LeaguePlayerLink.CreateWaitingLink(command.LeagueId, playerId)
		            : LeaguePlayerLink.CreateJoinedLink(command.LeagueId, playerId);

	            await lplinksRepo.AddAsync(link);
            }
            else if (lastLink.InWaitingStatus())
            {
                if (leagueSettings.RequireConfirmation)
                {
                    lplinksRepo.RemoveAsync(lastLink);
                }
                else
                {
	                lastLink.UpdateToJoined();
                    lplinksRepo.UpdateAsync(lastLink);
                }
            }
            else
            {
	            lastLink.UpdateToLeft();
                lplinksRepo.UpdateAsync(lastLink);
            }

            _uow.SaveChanges();
        }
    }
}
