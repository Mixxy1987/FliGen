using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Tours
{
    [MessageNamespace("tours")]
    public class TourBack : ICommand
    {
        public int TourId { get; }

        [JsonConstructor]
        public TourBack(int tourId)
        {
            TourId = tourId;
        }
    }
}