using FliGen.Common.Messages;
using Newtonsoft.Json;
using System;

namespace FliGen.Services.Teams.Application.Events
{
    public class TeamsCreated : IEvent
    {
        public int[][] Teams { get; }
        public int TourId { get; }
        public int LeagueId { get; }

        [JsonConstructor]
        public TeamsCreated(int[][] teams, int tourId, int leagueId)
        {
            Teams = teams;
            TourId = tourId;
            LeagueId = leagueId;
        }
    }
}