using FliGen.Services.Seasons.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesSeasonsIdQuery
{
    public class LeaguesSeasonsIdQuery : IRequest<IEnumerable<LeaguesSeasonsIdDto>>
    {
        public int[] SeasonsId { get; set; }
        public int[] LeaguesId { get; set; }
        public LeaguesSeasonsIdQueryType LeaguesSeasonsIdQueryType { get; set; }
    }
}