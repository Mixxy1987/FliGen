using FliGen.Common.Types;
using MediatR;
using System.Collections.Generic;
using FliGen.Services.Seasons.Application.Dto;

namespace FliGen.Services.Seasons.Application.Queries.Seasons
{
    /*
     * Get short information about seasons
     * SeasonsId == null - about all seasons;
     * SeasonsId contains one id == 0 - about currentseasons in all leagues in LeaguesId(or all leagues if null)
     * LeaguesId is not null - filter for this leagues
     */
    public class SeasonsQuery : PagedQuery,  IRequest<IEnumerable<SeasonDto>>
    {
        public int[] SeasonsId { get; set; }
        public int[] LeaguesId { get; set; }
    }
}