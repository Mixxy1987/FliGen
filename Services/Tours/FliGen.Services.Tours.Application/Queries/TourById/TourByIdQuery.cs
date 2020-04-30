using MediatR;

namespace FliGen.Services.Tours.Application.Queries.TourById
{
    public class TourByIdQuery : IRequest<Dto.TourDto>
    {
        public int TourId { get; set; }
    }
}