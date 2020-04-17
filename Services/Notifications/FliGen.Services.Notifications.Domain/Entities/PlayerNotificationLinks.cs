using FliGen.Common.SeedWork;
using FliGen.Services.Notifications.Domain.Entities.Enum;

namespace FliGen.Services.Notifications.Domain.Entities
{
    public class PlayerNotificationLink
    {
        public int PlayerId { get; set; }

        public int NotificationTypeId
        {
            get => NotificationType.Id;
            set => NotificationType = Enumeration.FromValue<NotificationType>(value);
        }

        public NotificationType NotificationType { get; private set; }
    }
}