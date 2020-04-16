namespace FliGen.Services.Seasons.Application.Dto
{
    public class Season
    {
        public string Start { get; set; }
        public int ToursPlayed { get; set; }
        public TourDto PreviousTour { get; set; }
        public TourDto NextTour { get; set; }
    }
}