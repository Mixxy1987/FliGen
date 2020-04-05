using System.Collections.Generic;
using FliGen.Services.Players.Application.Dto;
using MediatR;

namespace FliGen.Services.Players.Application.Queries.Players
{
    /*
     * 1) get actual rates
     * 2) get all rates
     * 3) only in concrete leagues
     * 4) 1,2,3 for concrete players
     */ //todo:: PagedQuery
    public class PlayersQuery : IRequest<IEnumerable<PlayerWithRate>>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public PlayersQueryType QueryType { get; set; }
        public int[] PlayerId { get; set; }
        public int[] LeagueId { get; set; }
    }
}
