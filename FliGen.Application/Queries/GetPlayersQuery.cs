using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries
{
    public class GetPlayersQuery : IRequest<GetPlayersResponse>
    {
    }
}
