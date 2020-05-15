using FliGen.Services.Seasons.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesIdBySeasonsId
{
    public class LeaguesIdBySeasonsIdQuery : IRequest<IEnumerable<LeagueIdBySeasonIdDto>>
    {
        public int[] SeasonsId { get; set; }
    }
}