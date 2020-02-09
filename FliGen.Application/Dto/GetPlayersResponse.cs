using FliGen.Domain.Entities;
using System.Collections.Generic;

namespace FliGen.Application.Dto
{
    public class GetPlayersResponse
    {
        public IEnumerable<Player> Players { get; set; }
    }
}
