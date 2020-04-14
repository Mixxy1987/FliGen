using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQuery : IRequest<IEnumerable<Dto.Tour>>
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public int PlayerId { get; set; }
        public ToursByPlayerIdQueryType QueryType { get; set; }
        public int[] SeasonIds { get; set; }
    }
}