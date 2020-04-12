using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Tours.Application.Events
{
    public class TourCanceled : IEvent
    {
        public int TourId { get; set; }

        [JsonConstructor]
        public TourCanceled(int tourId)
        {
            TourId = tourId;
        }
    }
}