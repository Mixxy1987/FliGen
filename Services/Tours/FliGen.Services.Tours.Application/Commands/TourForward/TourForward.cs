using FliGen.Common.Messages;

namespace FliGen.Services.Tours.Application.Commands.TourForward
{
    [MessageNamespace("tours")]
    public class TourForward : ICommand
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int? TourId { get; set; }
        public string Date { get; set; }
    }
}