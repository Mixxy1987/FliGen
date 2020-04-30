using MediatR;
using System.Collections.Generic;
using FliGen.Common.Types;

namespace FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQuery : PagedQuery, IRequest<IEnumerable<Dto.TourDto>>
    {
        public int PlayerId { get; set; }
        public int[] SeasonIds { get; set; }
        public ToursByPlayerIdQueryType QueryType { get; set; }
    }
}