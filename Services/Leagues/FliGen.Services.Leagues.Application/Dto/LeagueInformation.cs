namespace FliGen.Services.Leagues.Application.Dto
{
    public class LeagueInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
        public int SeasonsCount { get; set; }
        public int ToursCount { get; set; }

        public Season CurrentSeason { get; set; }
    }
}