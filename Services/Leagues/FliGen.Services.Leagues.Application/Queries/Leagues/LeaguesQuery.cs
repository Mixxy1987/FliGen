using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Leagues.Application.Queries.Leagues
{
    /*
     * Get short information about leagues
     * if PlayerExternalId is set than filter information for this player
     * if PlayerInternalId is set than filter information for this players(only when PlayerExternalId is not set)
     * if leagueId[] is not empty than filter information for this leagues 
     */ //todo:: PagedQuery
    public class LeaguesQuery : IRequest<IEnumerable<Dto.LeagueDto>>
    {
        public string PlayerExternalId { get; set; }
        public int[] LeagueId { get; set; }
        public int[] Pid { get; set; } // PlayerInternalId
    }
}