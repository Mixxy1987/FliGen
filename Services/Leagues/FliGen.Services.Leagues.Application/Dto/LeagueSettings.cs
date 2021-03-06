﻿namespace FliGen.Services.Leagues.Application.Dto
{
    public class LeagueSettings
    {
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }
    }
}