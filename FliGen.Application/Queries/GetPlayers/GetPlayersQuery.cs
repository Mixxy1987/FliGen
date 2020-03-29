using System.Collections.Generic;
using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Queries.GetPlayers
{
    /*
     * 1) get actual rates
     * 2) get all rates
     * 3) only in concrete leagues
     */
    public class GetPlayersQuery : IRequest<IEnumerable<PlayerWithRate>>
    {

    }
}
