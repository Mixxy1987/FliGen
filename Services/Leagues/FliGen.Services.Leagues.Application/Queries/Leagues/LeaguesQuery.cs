using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Leagues.Application.Queries.Leagues
{
    public class LeaguesQuery : IRequest<IEnumerable<Dto.League>>
    {
        public string PlayerId { get; set; }
    }
}