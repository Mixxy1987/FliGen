using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Tours.Application.Events
{
    public class TourCreated : IEvent
    {
        public int TourId { get; set; }

        [JsonConstructor]
        public TourCreated(int tourId)
        {
            TourId = tourId;
        }
    }
}