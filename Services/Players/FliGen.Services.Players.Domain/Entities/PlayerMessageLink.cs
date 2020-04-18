using FliGen.Common.SeedWork;
using FliGen.Services.Players.Domain.Entities.Enum;

namespace FliGen.Services.Players.Domain.Entities
{
    public class PlayerMessageLink
    {
        public int PlayerId { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public int MessageTypeId
        {
            get => MessageType.Id;
            set => MessageType = Enumeration.FromValue<MessageType>(value);
        }

        public MessageType MessageType { get; private set; }
        public bool Read { get; set; }
    }
}