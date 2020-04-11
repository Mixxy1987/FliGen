namespace FliGen.Services.Teams.Application.Dto
{
    public class LeagueSettingsDto
    {
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }
    }
}