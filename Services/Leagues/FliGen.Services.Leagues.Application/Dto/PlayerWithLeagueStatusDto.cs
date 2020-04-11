using FliGen.Services.Leagues.Application.Dto.Enum;

namespace FliGen.Services.Leagues.Application.Dto
{
    public class PlayerWithLeagueStatusDto
    {
        public int Id { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinStatus { get; set; }
        public int LeaguePlayerPriority { get; set; }
    }
}