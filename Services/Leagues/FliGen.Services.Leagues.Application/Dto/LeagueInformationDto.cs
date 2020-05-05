namespace FliGen.Services.Leagues.Application.Dto
{
    public class LeagueInformationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
        public int SeasonsCount { get; set; }
        public int ToursCount { get; set; }

        public SeasonDto CurrentSeason { get; set; }
    }
}