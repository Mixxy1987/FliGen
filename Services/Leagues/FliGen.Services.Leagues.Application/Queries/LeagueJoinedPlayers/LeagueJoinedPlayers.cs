using FliGen.Services.Leagues.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Leagues.Application.Queries.LeagueJoinedPlayers
{
    /// <summary>
    /// Get joined players for league with leagueId
    /// </summary>
    public class LeagueJoinedPlayers : IRequest<IEnumerable<PlayerInternalIdDto>>
    {
        public int LeagueId { get; set; }
    }
}