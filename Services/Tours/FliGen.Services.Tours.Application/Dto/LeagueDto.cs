using System.Collections.Generic;
using FliGen.Services.Tours.Application.Dto.Enum;

namespace FliGen.Services.Tours.Application.Dto
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueTypeDto LeagueTypeDto { get; set; }
        public int PlayerInternalId { get; set; }
        public IEnumerable<PlayerWithLeagueStatusDto> PlayersLeagueStatuses { get; set; }
    }
}