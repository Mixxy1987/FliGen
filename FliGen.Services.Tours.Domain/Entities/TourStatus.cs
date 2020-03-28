using FliGen.Common.SeedWork;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class TourStatus : Enumeration
    {
        public static readonly TourStatus Planned = new TourStatus(Enum.Planned, nameof(Planned));
        public static readonly TourStatus RegistrationOpened = new TourStatus(Enum.RegistrationOpened, nameof(RegistrationOpened));
        public static readonly TourStatus RegistrationClosed = new TourStatus(Enum.RegistrationClosed, nameof(RegistrationClosed));
        public static readonly TourStatus InProgress = new TourStatus(Enum.InProgress, nameof(InProgress));
        public static readonly TourStatus Completed = new TourStatus(Enum.Completed, nameof(Completed));
        public static readonly TourStatus Canceled = new TourStatus(Enum.Canceled, nameof(Canceled));

        public TourStatus(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
            public const int Planned = 1;
            public const int RegistrationOpened = 2;
            public const int RegistrationClosed = 3;
            public const int InProgress = 4;
            public const int Completed = 5;
            public const int Canceled = 6;
        }
    }
}