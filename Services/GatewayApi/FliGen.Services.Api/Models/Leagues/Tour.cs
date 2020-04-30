using FliGen.Services.Api.Models.Leagues.Enum;

namespace FliGen.Services.Api.Models.Leagues
{
    public class Tour
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string Date { get; set; }
        public int HomeCount { get; set; }
        public int GuestCount { get; set; }
        public TourStatus TourStatus { get; set; }
    }
}
