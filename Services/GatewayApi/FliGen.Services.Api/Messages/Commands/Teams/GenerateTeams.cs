using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Teams
{
    [MessageNamespace("teams")]
    public class GenerateTeams : ICommand
    {
        public int TourId { get; set; }
        public int LeagueId { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }
        public int[] Pid { get; set; }

        private GenerateTeams() { }

        [JsonConstructor]
        public GenerateTeams(int tourId, int leagueId, int? playersInTeam, int? teamsInTour, int[] pid)
        {
            TourId = tourId;
            LeagueId = leagueId;
            PlayersInTeam = playersInTeam;
            TeamsInTour = teamsInTour;
            Pid = pid;
        }
    }
}