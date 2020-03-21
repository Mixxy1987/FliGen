using MediatR;
using System.Collections.Generic;

namespace FliGen.Application.Queries.GetLeagues
{
    public class GetLeaguesQuery : IRequest<IEnumerable<Dto.League>>
    {
        public string UserId { get; }

        public GetLeaguesQuery(string userId)
        {
            UserId = userId;
        }
    }
}