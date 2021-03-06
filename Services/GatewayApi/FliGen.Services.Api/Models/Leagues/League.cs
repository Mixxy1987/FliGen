﻿using System.Collections.Generic;

namespace FliGen.Services.Api.Models.Leagues
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
        public IEnumerable<PlayerWithLeagueStatus> PlayersLeagueStatuses { get; set; }
    }
}