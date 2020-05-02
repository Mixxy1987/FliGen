using FliGen.Services.Api.Models.Tours;

namespace FliGen.Services.Api.Models.Seasons
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string Start { get; set; }
        public int ToursPlayed { get; set; }
        public Tour PreviousTour { get; set; }
        public Tour NextTour { get; set; }
    }
}