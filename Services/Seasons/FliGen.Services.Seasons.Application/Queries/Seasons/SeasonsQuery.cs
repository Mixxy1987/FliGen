using FliGen.Common.Types;
using FliGen.Services.Seasons.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Seasons.Application.Queries.Seasons
{
    /*
     * Get short information about seasons
     * SeasonsId == null - about all seasons;
     * LeagueId is set - filter for this league
     */
    public class SeasonsQuery : PagedQuery,  IRequest<IEnumerable<SeasonDto>>
    {
        public int[] SeasonsId { get; set; }
        public int LeagueId { get; set; }
        public SeasonsQueryType QueryType { get; set; }
    }
}