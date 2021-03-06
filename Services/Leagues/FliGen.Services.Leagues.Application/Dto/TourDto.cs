﻿using FliGen.Services.Leagues.Application.Dto.Enum;

namespace FliGen.Services.Leagues.Application.Dto
{
    public class TourDto
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string Date { get; set; }
        public int HomeCount { get; set; }
        public int GuestCount { get; set; }
        public TourStatus TourStatus { get; set; }
    }
}
