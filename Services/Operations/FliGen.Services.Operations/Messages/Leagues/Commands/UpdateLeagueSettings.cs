using FliGen.Common.Messages;

namespace FliGen.Services.Operations.Messages.Leagues.Commands
{
    [MessageNamespace("leagues")]
    public class UpdateLeagueSettings : ICommand
    {
        public int LeagueId { get; set; }
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
        public int PlayersInTeam { get; set; }
        public int TeamsInTour { get; set; }
    }
}