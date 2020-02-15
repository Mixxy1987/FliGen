using MediatR;

namespace FliGen.Application.Commands.Tour.AddTour
{
    public class AddTourCommand : IRequest
    {
        public string Date { get; set; }
    }
}