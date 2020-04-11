using FliGen.Services.Teams.Application.Dto.Enum;

namespace FliGen.Services.Teams.Application.Dto
{
    public class PlayerWithLeagueStatusDto
    {
        public int Id { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinStatus { get; set; }
        public int LeaguePlayerPriority { get; set; }
    }
}