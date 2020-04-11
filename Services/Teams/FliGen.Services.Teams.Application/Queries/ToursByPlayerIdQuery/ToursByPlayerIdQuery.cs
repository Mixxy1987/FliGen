using FliGen.Services.Teams.Application.Dto;
using MediatR;

namespace FliGen.Services.Teams.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQuery : IRequest<ToursByPlayerIdDto>
    {
        public int Page { get; }
        public int Size { get; }
        public int PlayerId { get; }
        
        public ToursByPlayerIdQuery(int size, int page, int playerId)
        {
            Size = size;
            Page = page;
            PlayerId = playerId;
        }
    }
}