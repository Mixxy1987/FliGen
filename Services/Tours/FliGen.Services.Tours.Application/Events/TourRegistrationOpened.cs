using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Tours.Application.Events
{
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