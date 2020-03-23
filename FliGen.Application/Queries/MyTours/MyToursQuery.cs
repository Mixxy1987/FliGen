using System.Collections.Generic;
using MediatR;

namespace FliGen.Application.Queries.Tour.MyTours
{
    public class MyToursQuery : IRequest<IEnumerable<Dto.MyTour>>
    {
        public string UserId { get; }

        public MyToursQuery(string userId)
        {
	        UserId = userId;
        }
    }
}