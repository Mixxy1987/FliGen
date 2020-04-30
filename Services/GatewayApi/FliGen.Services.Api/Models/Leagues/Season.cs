namespace FliGen.Services.Api.Models.Leagues
{
    public class Season
    {
        public string Start { get; set; }
        public int ToursPlayed { get; set; }
        public Tour PreviousTour { get; set; }
        public Tour NextTour { get; set; }
    }
}