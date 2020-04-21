using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Leagues.Application.Events
{
    public class LeagueSettingsUpdated : IEvent
    {
        public int LeagueId { get; set; }

        [JsonConstructor]
        public LeagueSettingsUpdated(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}