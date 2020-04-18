using FliGen.Common.SeedWork;

namespace FliGen.Services.Players.Domain.Entities.Enum
{
    public class MessageType : Enumeration
    {
        public static MessageType Personal = new MessageType(Enum.Personal, nameof(Personal));
        public static MessageType All = new MessageType(Enum.All, nameof(All));

        public MessageType(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
            public const int Personal = 1;
            public const int All = 2;
        }
    }
}