using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Players.Application.Events
{
    public class PlayerDeleted : IEvent
    {
        [JsonConstructor]
        public PlayerDeleted()
        {
        }
    }
}