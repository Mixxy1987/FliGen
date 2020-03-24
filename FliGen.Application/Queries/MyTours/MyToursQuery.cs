using System.Collections.Generic;
using MediatR;

namespace FliGen.Application.Queries.MyTours
{
    public class MyToursQuery : IRequest<IEnumerable<Dto.Tour>>
    {
        public string UserId { get; }

        public MyToursQuery(string userId)
        {
	        UserId = userId;
        }
    }
}