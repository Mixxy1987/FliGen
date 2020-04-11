using FliGen.Common.Messages;

namespace FliGen.Services.Teams.Application.Commands.GenerateTeams
{
    [MessageNamespace("teams")]
    public class GenerateTeams : ICommand
    {
        public int TourId { get; set; }
        public int LeagueId { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour{ get; set; }
        public int[] Pid { get; set; }

        public GenerateTeamsStrategy GenerateTeamsStrategy { get; set; } = GenerateTeamsStrategy.Random;
    }
}