using FliGen.Services.Teams.Application.Dto;
using MediatR;

namespace FliGen.Services.Teams.Application.Queries
{
    public class ToursByPlayerIdQuery : IRequest<ToursByPlayerIdDto>
    {
        public int Size { get; }
        public int PlayerId { get; }
        
        public ToursByPlayerIdQuery(int playerId, int size)
        {
            PlayerId = playerId;
            Size = size;
        }
    }
}