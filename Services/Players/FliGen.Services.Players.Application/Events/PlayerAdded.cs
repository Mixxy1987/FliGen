using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Players.Application.Events
{
    public class PlayerAdded : IEvent
    {
        public int InternalId { get; }

        [JsonConstructor]
        public PlayerAdded(int internalId)
        {
            InternalId = internalId;
        }
    }
}