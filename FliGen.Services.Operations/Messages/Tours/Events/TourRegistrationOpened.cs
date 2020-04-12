using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
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