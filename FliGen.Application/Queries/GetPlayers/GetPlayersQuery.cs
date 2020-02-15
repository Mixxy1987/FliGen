using System.Collections.Generic;
using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries.GetPlayers
{
    public class GetPlayersQuery : IRequest<IEnumerable<PlayerWithRate>>
    {
    }
}
