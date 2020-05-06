using FliGen.Common.Messages;

namespace FliGen.Services.Players.Application.Events.PlayerRegistered
{
    [MessageNamespace("web")]
    public class PlayerRegistered : IEvent
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}