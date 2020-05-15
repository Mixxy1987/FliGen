using MediatR;

namespace FliGen.Services.Leagues.Application.Queries.PlayerJoinedLeagues
{
    /// <summary>
    /// Get leagues by playerId with joined status
    /// </summary>
    public class PlayerJoinedLeagues : IRequest<int[]>
    {
        public int PlayerId { get; set; }
    }
}