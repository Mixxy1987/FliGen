using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Tours
{
    [MessageNamespace("tours")]
    public class TourForward : ICommand
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int? TourId { get; set; }
        public string Date { get; set; }

        private TourForward() { }

        [JsonConstructor]
        public TourForward(int leagueId, int seasonId, int? tourId, string date)
        {
            LeagueId = leagueId;
            SeasonId = seasonId;
            TourId = tourId;
            Date = date;
        }
    }
}