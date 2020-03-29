using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Tours
{
    [MessageNamespace("tours")]
    public class TourCancel : ICommand
    {
        public int TourId { get; }

        [JsonConstructor]
        public TourCancel(int tourId)
        {
            TourId = tourId;
        }
    }
}