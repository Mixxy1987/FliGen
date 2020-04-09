using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Tours
{
    [MessageNamespace("tours")]
    public class PlayerRegisterOnTour : ICommand
    {
        public string PlayerExternalId { get; set; }
        public int[] PlayerInternalIds { get; set; }
        public int LeagueId { get; set; }
        public int TourId { get; set; }

        private PlayerRegisterOnTour() {}

        [JsonConstructor]
        public PlayerRegisterOnTour(string playerExternalId, int[] playerInternalIds, int leagueId, int tourId)
        {
            PlayerExternalId = playerExternalId;
            PlayerInternalIds = playerInternalIds;
            LeagueId = leagueId;
            TourId = tourId;
        }
    }
}