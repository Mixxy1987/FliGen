using MediatR;

namespace FliGen.Application.Commands.Tour.TourForwardCommand
{
    public class TourForwardCommand : IRequest
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int? TourId { get; set; }
        public string Date { get; set; }
    }
}