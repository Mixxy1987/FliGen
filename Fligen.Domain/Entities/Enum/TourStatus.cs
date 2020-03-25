using FliGen.Common.SeedWork;

namespace FliGen.Domain.Entities.Enum
{
    public class TourStatus : Enumeration
    {
        public static TourStatus Planned = new TourStatus(1, nameof(Planned));
        public static TourStatus RegistrationOpened = new TourStatus(2, nameof(RegistrationOpened));
        public static TourStatus RegistrationClosed = new TourStatus(3, nameof(RegistrationClosed));
        public static TourStatus InProgress = new TourStatus(4, nameof(InProgress)); 
        public static TourStatus Completed = new TourStatus(5, nameof(Completed));
        public static TourStatus Canceled = new TourStatus(6, nameof(Canceled));

        public TourStatus(int id, string name) : base(id, name)
        {
        }
    }
}