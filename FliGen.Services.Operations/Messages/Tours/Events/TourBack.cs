using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
{
    [MessageNamespace("tours")]
    public class TourBack : IEvent
    {
        public int TourId { get; set; }

        [JsonConstructor]
        public TourBack(int tourId)
        {
            TourId = tourId;
        }
    }
}