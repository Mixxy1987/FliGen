using FliGen.Common.SeedWork;

namespace FliGen.Services.Notifications.Domain.Entities.Enum
{
    public class NotificationType : Enumeration
    {
        public static NotificationType TourRegistrationOpened = new NotificationType(Enum.TourRegistrationOpened, nameof(TourRegistrationOpened));
        public static NotificationType TourRegistrationClosed = new NotificationType(Enum.TourRegistrationClosed, nameof(TourRegistrationClosed));

        public NotificationType(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
	        public const int TourRegistrationOpened = 1;
	        public const int TourRegistrationClosed = 2;
        }
    }
}