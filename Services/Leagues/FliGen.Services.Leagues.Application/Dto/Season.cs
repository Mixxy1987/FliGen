namespace FliGen.Services.Leagues.Application.Dto
{
    public class Season
    {
        public string Start { get; set; }
        public int ToursPlayed { get; set; }
        public Tour PreviousTour { get; set; }
        public Tour NextTour { get; set; }
    }
}