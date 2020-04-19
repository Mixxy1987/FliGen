using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Notifications.Application.Events.TeamsCreated
{
    [MessageNamespace("teams")]
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