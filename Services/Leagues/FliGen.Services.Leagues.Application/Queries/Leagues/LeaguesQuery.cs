using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Leagues.Application.Queries.Leagues
{
    /*
     * Get short information about league
     * if PlayerExternalId is set than filter information for this player
     * if leagueId[] is not empty than filter information for this leagues 
     */ //todo:: PagedQuery
    public class LeaguesQuery : IRequest<IEnumerable<Dto.League>>
    {
        public string PlayerExternalId { get; set; }
        public int[] LeagueId { get; set; }
    }
}