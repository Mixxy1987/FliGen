using FliGen.Common.Types;
using MediatR;

namespace FliGen.Services.Teams.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQuery : PagedQuery, IRequest<PagedResult<int>>
    {
        public int PlayerId { get; }
        
        public ToursByPlayerIdQuery(int size, int page, int playerId): base(size, page)
        {
            PlayerId = playerId;
        }
    }
}