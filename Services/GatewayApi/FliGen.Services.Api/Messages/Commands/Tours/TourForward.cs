﻿using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Tours
{
    [MessageNamespace("tours")]
    public class TourForward : ICommand
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int? TourId { get; set; }
        public string Date { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }
        public GenerateTeamsStrategy GenerateTeamsStrategy { get; set; }

        private TourForward(GenerateTeamsStrategy generateTeamsStrategy)
        {
            GenerateTeamsStrategy = generateTeamsStrategy;
        }

        [JsonConstructor]
        public TourForward(
            int leagueId,
            int seasonId,
            int? tourId,
            string date,
            int? playersInTeam, 
            int? teamsInTour,
            GenerateTeamsStrategy generateTeamsStrategy)
        {
            LeagueId = leagueId;
            SeasonId = seasonId;
            TourId = tourId;
            Date = date;
            PlayersInTeam = playersInTeam;
            TeamsInTour = teamsInTour;
            GenerateTeamsStrategy = generateTeamsStrategy;
        }
    }
}