using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Leagues.Application.Events
{
    public class LeagueUpdated : IEvent
    {
        public int LeagueId { get; set; }

        [JsonConstructor]
        public LeagueUpdated(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}