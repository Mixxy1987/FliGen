using FliGen.Common.Types;
using FliGen.Services.Tours.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    /*
     * Last - if querytype == last
     * SeasonsId - filter by seasons
     */
    public class ToursQuery : PagedQuery, IRequest<IEnumerable<TourDto>>
    {
        public int Last{ get; set; }
        public int[] SeasonsId { get; set; }
        public ToursQueryType ToursQueryType { get; set; }
    }
}