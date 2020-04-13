using FliGen.Common.Messages;

namespace FliGen.Services.Tours.Application.Commands.TourBack
{
    [MessageNamespace("tours")]
    public class TourBack : ICommand
    {
        public int TourId { get; set; }
    }
}