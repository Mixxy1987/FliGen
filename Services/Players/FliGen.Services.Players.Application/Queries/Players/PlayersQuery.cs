using FliGen.Common.Types;
using FliGen.Services.Players.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Players.Application.Queries.Players
{
    /*
     * 1) get actual rates
     * 2) get all rates
     * 3) only in concrete leagues
     * 4) 1,2,3 for concrete players
     */
    public class PlayersQuery : PagedQuery, IRequest<IEnumerable<PlayerWithRate>>
    {
        public PlayersQueryType QueryType { get; set; }
        public int[] PlayerId { get; set; }
        public int[] LeagueId { get; set; }
    }
}
