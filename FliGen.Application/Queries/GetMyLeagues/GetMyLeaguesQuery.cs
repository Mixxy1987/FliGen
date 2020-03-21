using System.Collections.Generic;
using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries.GetMyLeagues
{
    public class GetMyLeaguesQuery : IRequest<IEnumerable<Dto.League>>
    {
        public string UserId { get;}

        public GetMyLeaguesQuery(string userId)
        {
	        UserId = userId;
        }
    }
}