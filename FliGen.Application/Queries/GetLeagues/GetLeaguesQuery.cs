using System.Collections.Generic;
using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries.GetLeagues
{
    public class GetLeaguesQuery : IRequest<IEnumerable<League>>
    {
        
    }
}