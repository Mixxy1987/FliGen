using System.Collections.Generic;
using MediatR;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public class MyToursQuery : IRequest<IEnumerable<Dto.Tour>>
    {
        public int UserId { get; }
        public int Size { get; }
        public MyToursQueryType QueryType { get; }
        public int[] SeasonIds { get; }
        
        public MyToursQuery(int userId, int size, MyToursQueryType queryType, int[] seasonId)
        {
            UserId = userId;
            Size = size;
            QueryType = queryType;
            SeasonIds = seasonId;
        }
    }
}