using FliGen.Common.Messages;
using FliGen.Services.Tours.Application.Dto.Enum;

namespace FliGen.Services.Tours.Application.Commands.TourForward
{
    [MessageNamespace("tours")]
    public class TourForward : ICommand
    {
        public int? TourId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string Date { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }

        public GenerateTeamsStrategy GenerateTeamsStrategy { get; set; } = GenerateTeamsStrategy.Random;
    }
}