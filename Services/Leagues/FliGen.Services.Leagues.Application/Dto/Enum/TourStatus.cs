namespace FliGen.Services.Leagues.Application.Dto.Enum
{
    public enum TourStatus
    {
        Planned, // => 1) RegistrationOpened 2) Canceled
        RegistrationOpened, // => 1) RegistrationClosed 2) Canceled
        RegistrationClosed, // => 1) InProgress 2) Canceled
        InProgress, // 1) => Completed
        Completed, // final status
        Canceled // 1) => Planned
    }
}