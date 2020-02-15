using System.Collections.Generic;
using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries.GetLeagueTypes
{
    public class GetLeagueTypesQuery : IRequest<IEnumerable<LeagueType>>
    {
        
    }
}