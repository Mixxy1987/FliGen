using System.Collections.Generic;
using MediatR;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public class MyToursQuery : IRequest<IEnumerable<Dto.Tour>>
    {
        public int PlayerId { get; }
        public int Size { get; }
        public MyToursQueryType QueryType { get; }
        public int[] SeasonIds { get; }
        
        public MyToursQuery(int playerId, int size, MyToursQueryType queryType, int[] seasonId)
        {
            PlayerId = playerId;
            Size = size;
            QueryType = queryType;
            SeasonIds = seasonId;
        }
    }
}