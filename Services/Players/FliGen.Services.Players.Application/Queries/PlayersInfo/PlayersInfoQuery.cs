using FliGen.Services.Players.Application.Dto;
using MediatR;

namespace FliGen.Services.Players.Application.Queries.PlayersInfo
{
    public class PlayersInfoQuery : PagedQuery, IRequest<PlayersInfoDto>
    {
    }
}
