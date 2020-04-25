using System.Collections.Generic;

namespace FliGen.Services.Leagues.Application.Dto
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
        public IEnumerable<PlayerWithLeagueStatusDto> PlayersLeagueStatuses { get; set; }
    }
}