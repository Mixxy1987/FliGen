using FliGen.Services.Tours.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Application.Queries.LeaguesSeasonsIdQuery
{
    public class LeaguesSeasonsIdQuery : IRequest<IEnumerable<LeaguesSeasonsIdDto>>
    {
        public int[] SeasonsId { get; set; }
        public int[] LeaguesId { get; set; }
        public LeaguesSeasonsIdQueryType QueryType { get; set; }
    }
}