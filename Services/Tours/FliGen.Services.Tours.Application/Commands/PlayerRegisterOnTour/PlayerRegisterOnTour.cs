using System;
using FliGen.Common.Messages;

namespace FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour
{
    /*
     * 1) player register by himself - PlayerExternalId
     * 2) bulk register by admin - PlayerInternalIds
     */
    [MessageNamespace("tours")]
    public class PlayerRegisterOnTour : ICommand
    {
        public string PlayerExternalId { get; set; }
        public int[] PlayerInternalIds { get; set; }
        public int LeagueId { get; set; }
        public int TourId { get; set; }
        public string RegistrationDate { get; set; }
    }
}