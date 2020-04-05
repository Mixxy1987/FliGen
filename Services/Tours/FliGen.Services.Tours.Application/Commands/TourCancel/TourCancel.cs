using FliGen.Common.Messages;

namespace FliGen.Services.Tours.Application.Commands.TourCancel
{
    [MessageNamespace("tours")]
    public class TourCancel : ICommand
    {
        public int TourId { get; set; }
    }
}