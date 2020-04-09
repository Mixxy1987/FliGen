using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Leagues
{
    [MessageNamespace("leagues")]
    public class UpdateLeagueSettings : ICommand
    {
        public int LeagueId { get; set; }
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
        public int PlayersInTeam { get; set; }
        public int TeamsInTour { get; set; }

        private UpdateLeagueSettings() { }

        [JsonConstructor]
        public UpdateLeagueSettings(int leagueId, bool visibility, bool requireConfirmation, int playersInTeam, int teamsInTour)
        {
            LeagueId = leagueId;
            Visibility = visibility;
            RequireConfirmation = requireConfirmation;
            PlayersInTeam = playersInTeam;
            TeamsInTour = teamsInTour;
        }
    }
}