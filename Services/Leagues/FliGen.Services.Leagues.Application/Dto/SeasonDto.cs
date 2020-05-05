namespace FliGen.Services.Leagues.Application.Dto
{
    public class SeasonDto
    {
        public int SeasonId { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public int ToursPlayed { get; set; }
        public TourDto PreviousTour { get; set; }
        public TourDto NextTour { get; set; }
    }
}