﻿namespace FliGen.Services.Leagues.Application.Dto
{
    public class PlayerLeagueRate
    {
        public string Date { get; set; }
        public int PlayerId { get; set; }
        public int LeagueId { get; set; }
        public double Rate { get; set; }
    }
}
