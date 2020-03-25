using MediatR;

namespace FliGen.Services.Tours.Application.Commands.TourCancelCommand
{
    public class TourCancelCommand : IRequest
    {
        public int TourId { get; set; }
    }
}