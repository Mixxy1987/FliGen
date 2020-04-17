using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Notifications.Application.Events.TourRegistrationOpened
{
    [MessageNamespace("tours")]
    public class TourRegistrationOpened : IEvent
    {
        public int TourId { get; set; }

        [JsonConstructor]
        public TourRegistrationOpened(int tourId)
        {
            TourId = tourId;
        }
    }
}