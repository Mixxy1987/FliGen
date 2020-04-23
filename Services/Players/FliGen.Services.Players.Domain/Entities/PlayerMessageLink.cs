using FliGen.Common.SeedWork;
using FliGen.Services.Players.Domain.Entities.Enum;

namespace FliGen.Services.Players.Domain.Entities
{
    public class PlayerMessageLink
    {
        public int PlayerId { get; }
        public int MessageId { get; }
        public virtual Message Message { get; }
        public int MessageTypeId
        {
            get => MessageType.Id;
            set => MessageType = Enumeration.FromValue<MessageType>(value);
        }

        public MessageType MessageType { get; private set; }
        public bool Read { get; private set; }

        public PlayerMessageLink()
        {
            MessageType ??= MessageType.Personal;
        }

        private PlayerMessageLink(int playerId, int messageId, int messageTypeId) : this()
        {
            PlayerId = playerId;
            MessageId = messageId;
            MessageTypeId = messageTypeId;
            Read = false;
        }

        public static PlayerMessageLink Create(int playerId, int messageId, int messageTypeId)
        {
            return new PlayerMessageLink(playerId, messageId, messageTypeId);
        }

        public void MarkAsRead()
        {
            Read = true;
        }
    }
}