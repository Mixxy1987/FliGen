using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Dto.Enum;
using FliGen.Services.Teams.Application.Events;
using FliGen.Services.Teams.Application.Queries.LeaguesQuery;
using FliGen.Services.Teams.Application.Queries.PlayersQuery;
using FliGen.Services.Teams.Application.Services;
using FliGen.Services.Teams.Application.Services.GenerateTeams;
using FliGen.Services.Teams.Domain.Common;
using FliGen.Services.Teams.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Application.Commands.GenerateTeams
{
    public class GenerateTeamsHandler : ICommandHandler<GenerateTeams>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPlayersService _playersService;
        private readonly ILeaguesService _leaguesService;
        private readonly IGenerateTeamsServiceFactory _generateTeamsServiceFactory;
        private readonly IBusPublisher _busPublisher;

        public GenerateTeamsHandler(
            IUnitOfWork uow,
            IPlayersService playersService,
            ILeaguesService leaguesService,
            IGenerateTeamsServiceFactory generateTeamsServiceFactory,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _playersService = playersService;
            _leaguesService = leaguesService;
            _generateTeamsServiceFactory = generateTeamsServiceFactory;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(GenerateTeams command, ICorrelationContext context)
        {
            (int playersInTeam, int teamsInTour) = await GetPlayersAndTeamsCount(command);
            List<PlayerWithLeagueStatusDto> playersWithLeagueStatus = await GetPlayersInformationFromLeagueService(command);
            List<PlayerWithRateDto> playersWithRate = await GetPlayersRate(command);

            ValidateDataOrThrow(
                playersWithLeagueStatus,
                playersWithRate,
                playersInTeam,
                teamsInTour);

            int[][] teams = GenerateTeams(
                playersWithLeagueStatus,
                playersWithRate,
                playersInTeam,
                teamsInTour,
                command.GenerateTeamsStrategy);

            CleanPreviousGeneratedTeams(command, teams);
            SaveGeneratedTeams(command, teams);

            await _busPublisher.PublishAsync(new TeamsCreated(teams, command.TourId, command.LeagueId), context);

            _uow.SaveChanges();
        }

        private void ValidateDataOrThrow(
            List<PlayerWithLeagueStatusDto> playersWithLeagueStatus,
            List<PlayerWithRateDto> playersWithRate,
            int playersInTeam,
            int teamsInTour)
        {
            //todo:: can we divide players?
            var readyPlayers =
                playersWithLeagueStatus.Where(x => x.PlayerLeagueJoinStatus == PlayerLeagueJoinStatus.Joined);

            if (readyPlayers.Count() < playersInTeam * teamsInTour)
            {
                throw new FliGenException(ErrorCodes.NotEnoughPlayers, "Not enough players.");
            }

            //todo:: another reasons to reject
        }

        private void CleanPreviousGeneratedTeams(GenerateTeams command, int[][] teams)
        {
            var ttplRepo = _uow.GetRepository<TemporalTeamPlayerLink>();
            var previousData = ttplRepo.GetList(x => x.TourId == command.TourId);
            ttplRepo.Delete(previousData.Items);
        }

        private void SaveGeneratedTeams(GenerateTeams command, int[][] teams)
        {
            var ttplRepo = _uow.GetRepository<TemporalTeamPlayerLink>();

            for (int i = 0; i < teams.Length; i++)
            {
                for (int j = 0; j < teams[i].Length; j++)
                {
                    var link = TemporalTeamPlayerLink.Create(command.TourId, command.LeagueId, i + 1, teams[i][j]);
                    ttplRepo.Add(link);
                }
            }
        }

        private int[][] GenerateTeams(
            IEnumerable<PlayerWithLeagueStatusDto> playersWithLeagueStatus,
            List<PlayerWithRateDto> playersWithRate, 
            int playersInTeam,
            int teamsInTour, 
            GenerateTeamsStrategy strategy)
        {
            var playerInfosForGenerate = new List<PlayerInfoForGenerate>();
            foreach (var player in playersWithLeagueStatus.Where(x => x.PlayerLeagueJoinStatus == PlayerLeagueJoinStatus.Joined))
            {
                var rate = playersWithRate.Single(x => x.Id == player.Id).PlayerLeagueRates.First().Rate;
                playerInfosForGenerate.Add(new PlayerInfoForGenerate(player.Id, rate, player.LeaguePlayerPriority));
            }

            return _generateTeamsServiceFactory
                .Create(strategy)
                .Generate(new InfoForGenerate(teamsInTour, playersInTeam, playerInfosForGenerate));
        }

        private async Task<List<PlayerWithRateDto>> GetPlayersRate(GenerateTeams command)
        {
            return (await _playersService.GetAsync(
                new PlayersQuery
                {
                    Size = command.Pid.Length,
                    PlayerId = command.Pid,
                    QueryType = PlayersQueryType.Actual,
                    LeagueId = new[] { command.LeagueId },
                })).ToList();
        }

        private async Task<List<PlayerWithLeagueStatusDto>> GetPlayersInformationFromLeagueService(GenerateTeams command)
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

            return leagueDto.PlayersLeagueStatuses.ToList();
        }

        private async Task<(int, int)> GetPlayersAndTeamsCount(GenerateTeams command)
        {
            if (command.PlayersInTeam is null || command.TeamsInTour is null)
            {
                var leagueSettings =
                    await _leaguesService.GetLeagueSettingsAsync(command.LeagueId);

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
