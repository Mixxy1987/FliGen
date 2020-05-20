using FliGen.Common.Types;
using FliGen.Services.Tours.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    /*
     * if playerId is set - filter for this player.
     * Last - if querytype == last
     * SeasonsId - filter by seasons
     */
    public class ToursQuery : PagedQuery, IRequest<IEnumerable<TourDto>>
    {
        public int PlayerId { get; set; }
        public int? Last { get; set; }
        public int[] SeasonsId { get; set; }
        public ToursQueryType QueryType { get; set; }
    }
}