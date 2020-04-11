using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Teams.Application.Common;
using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Queries.LeaguesQuery;
using FliGen.Services.Teams.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Services.Teams.Application.Dto.Enum;
using FliGen.Services.Teams.Application.Queries.PlayersQuery;
using FliGen.Services.Teams.Application.Services.GenerateTeams;

namespace FliGen.Services.Teams.Application.Commands.GenerateTeams
{
    public class GenerateTeamsHandler : ICommandHandler<GenerateTeams>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPlayersService _playersService;
        private readonly ILeaguesService _leaguesService;
        private readonly IGenerateTeamsServiceFactory _generateTeamsServiceFactory;

        public GenerateTeamsHandler(
            IUnitOfWork uow,
            IPlayersService playersService,
            ILeaguesService leaguesService,
            IGenerateTeamsServiceFactory generateTeamsServiceFactory)
        {
            _uow = uow;
            _playersService = playersService;
            _leaguesService = leaguesService;
            _generateTeamsServiceFactory = generateTeamsServiceFactory;
        }

        public async Task HandleAsync(GenerateTeams command, ICorrelationContext context)
        {
            (int playersInTeam, int teamsInTour) = await GetPlayersAndTeamsCount(command);

            IEnumerable<PlayerWithLeagueStatusDto> playersWithLeagueStatus = await GetPlayersInformationFromLeagueService(command);
            List<PlayerWithRateDto> playersWithRate = (await GetPlayersRate(command)).ToList();

            var playerInfosForGenerate = new List<PlayerInfoForGenerate>();
            foreach (var player in playersWithLeagueStatus.Where(x => x.PlayerLeagueJoinStatus == PlayerLeagueJoinStatus.Joined))
            {
                var rate = playersWithRate.Single(x => x.Id == player.Id).PlayerLeagueRates.First().Rate;
                playerInfosForGenerate.Add(new PlayerInfoForGenerate(player.Id, rate, player.LeaguePlayerPriority));
            }

            IGenerateTeamsService generateTeamsService = 
                _generateTeamsServiceFactory.Create(command.GenerateTeamsStrategy);
            var teams = generateTeamsService.Generate(
                new InfoForGenerate(teamsInTour, playersInTeam, playerInfosForGenerate));

            _uow.SaveChanges();
        }

        private async Task<IEnumerable<PlayerWithRateDto>> GetPlayersRate(GenerateTeams command)
        {
            return await _playersService.GetAsync(
                new PlayersQuery
                {
                    Size = command.Pid.Length,
                    PlayerId = command.Pid,
                    QueryType = PlayersQueryType.Actual,
                    LeagueId = new[] { command.LeagueId },
                });
        }

        private async Task<IEnumerable<PlayerWithLeagueStatusDto>> GetPlayersInformationFromLeagueService(GenerateTeams command)
        {
            var leagueDto = (await _leaguesService.GetAsync(
                new LeaguesQuery
                {
                    Pid = command.Pid,
                    LeagueId = new[] { command.LeagueId }
                }))
                .FirstOrDefault();

            if (leagueDto is null)
            {
                throw new FliGenException(ErrorCodes.NoInformationAboutLeague, "No information about players.");
            }

            return leagueDto.PlayersLeagueStatuses;
        }

        private async Task<(int, int)> GetPlayersAndTeamsCount(GenerateTeams command)
        {
            if (command.PlayersInTeam is null || command.TeamsInTour is null)
            {
                var leagueSettings =
                    await _leaguesService.GetLeagueSettings(command.LeagueId);

                if (leagueSettings.PlayersInTeam is null)
                {
                    throw new FliGenException(ErrorCodes.NoInformationAboutPlayersInTeamCount, "Please set players in team count.");
                }
                if (leagueSettings.TeamsInTour is null)
                {
                    throw new FliGenException(ErrorCodes.NoInformationAboutTeamsInTourCount, "Please set teams in tour count.");
                }

                return ((int)leagueSettings.PlayersInTeam, (int)leagueSettings.TeamsInTour);
            }

            return ((int)command.PlayersInTeam, (int)command.TeamsInTour);
        }
    }
}
