using System.Collections.Generic;
using MediatR;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public class MyToursQuery : IRequest<IEnumerable<Dto.Tour>>
    {
        public int UserId { get; }
        public int Size { get; }

        public MyToursQuery(int userId, int size)
        {
            UserId = userId;
            Size = size;
        }
    }
}