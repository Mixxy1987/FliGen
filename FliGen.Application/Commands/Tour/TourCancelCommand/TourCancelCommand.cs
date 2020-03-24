using MediatR;

namespace FliGen.Application.Commands.Tour.TourCancelCommand
{
    public class TourCancelCommand : IRequest
    {
        public int TourId { get; set; }
    }
}