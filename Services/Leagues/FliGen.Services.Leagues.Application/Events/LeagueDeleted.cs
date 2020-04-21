using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Leagues.Application.Events
{
    public class LeagueDeleted : IEvent
    {
        [JsonConstructor]
        public LeagueDeleted()
        {
        }
    }
}