﻿using FliGen.Common.Messages;
using FliGen.Services.Operations.Messages.Tours.Commands;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
{
    [MessageNamespace("tours")]
    public class TourRegistrationClosed : IEvent
    {
        public int TourId { get; set; }
        public int LeagueId { get; set; }
        public int? PlayersInTeam { get; set; }
        public int? TeamsInTour { get; set; }
        public int[] Pid { get; set; }
        public GenerateTeamsStrategy GenerateTeamsStrategy { get; set; }

        [JsonConstructor]
        public TourRegistrationClosed(
            int tourId,
            int leagueId,
            int? playersInTeam,
            int? teamsInTour,
            int[] pid,
            GenerateTeamsStrategy generateTeamsStrategy)
        {
            TourId = tourId;
            LeagueId = leagueId;
            PlayersInTeam = playersInTeam;
            TeamsInTour = teamsInTour;
            Pid = pid;
            GenerateTeamsStrategy = generateTeamsStrategy;
        }
    }
}