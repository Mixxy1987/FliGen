using FliGen.Services.Api.Models.Leagues.Enum;

namespace FliGen.Services.Api.Models.Leagues
{
    public class PlayerWithLeagueStatus
    {
        public int Id { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinStatus { get; set; }
    }
}