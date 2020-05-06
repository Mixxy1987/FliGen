using FliGen.Common.Messages;

namespace FliGen.Web.Event
{
    public class PlayerRegistered : IEvent
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PlayerRegistered(string externalId, string firstName, string lastName)
        {
            ExternalId = externalId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}