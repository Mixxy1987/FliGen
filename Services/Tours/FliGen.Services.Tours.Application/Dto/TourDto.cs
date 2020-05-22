using FliGen.Services.Tours.Application.Dto.Enum;

namespace FliGen.Services.Tours.Application.Dto
{
    public class TourDto
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int LeagueId { get; set; }
        public string Date { get; set; }
        public int HomeCount { get; set; }
        public int GuestCount { get; set; }
        public TourStatus TourStatus { get; set; }
        public bool PlayerRegistered { get; set; } // if request done by authorized player and status is incoming
    }
}
