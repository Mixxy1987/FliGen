using System.Collections.Generic;
using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.LeagueTypes
{
    public class LeagueTypesQuery : IRequest<IEnumerable<LeagueType>>
    {
        
    }
}