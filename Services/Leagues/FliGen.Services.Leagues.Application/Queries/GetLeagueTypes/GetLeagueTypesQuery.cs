using System.Collections.Generic;
using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.GetLeagueTypes
{
    public class GetLeagueTypesQuery : IRequest<IEnumerable<LeagueType>>
    {
        
    }
}