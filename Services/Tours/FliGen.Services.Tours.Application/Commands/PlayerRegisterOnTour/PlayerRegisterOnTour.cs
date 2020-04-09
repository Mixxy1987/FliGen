using FliGen.Common.Messages;

namespace FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour
{
    [MessageNamespace("tours")]
    public class PlayerRegisterOnTour : ICommand
    {
        public string PlayerExternalId { get; set; }
        public int[] PlayerInternalIds { get; set; }
        public int LeagueId { get; set; }
        public int TourId { get; set; }
    }
}