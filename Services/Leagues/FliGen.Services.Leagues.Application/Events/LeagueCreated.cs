using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Leagues.Application.Events
{
    public class LeagueCreated : IEvent
    {
        public int LeagueId { get; set; }

        [JsonConstructor]
        public LeagueCreated(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}