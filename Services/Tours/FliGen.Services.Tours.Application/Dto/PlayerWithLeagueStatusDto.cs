using FliGen.Services.Tours.Application.Dto.Enum;

namespace FliGen.Services.Tours.Application.Dto
{
    public class PlayerWithLeagueStatusDto
    {
        public int Id { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinStatus { get; set; }
        public int LeaguePlayerPriority { get; set; }
    }
}