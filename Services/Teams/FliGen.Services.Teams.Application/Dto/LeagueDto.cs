using System.Collections.Generic;

namespace FliGen.Services.Teams.Application.Dto
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueTypeDto LeagueTypeDto { get; set; }
        public IEnumerable<PlayerWithLeagueStatusDto> PlayersLeagueStatuses { get; set; }
    }
}