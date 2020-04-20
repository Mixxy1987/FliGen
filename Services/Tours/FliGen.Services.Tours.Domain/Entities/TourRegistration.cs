using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Tours.Domain.Common;
using System;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class TourRegistration : Entity
    {
        public int TourId { get; set; }
        public int PlayerId { get; set; }
        public DateTime RegistrationDate { get; set; }

        private TourRegistration()
        { }

        private TourRegistration(int tourId, int playerId, DateTime registrationDate)
        {
            if (tourId <= 0)
            {
                throw new FliGenException(ErrorCodes.InvalidTourId, $"Invalid tourId - {tourId}");
            }
            if (playerId <= 0)
            {
                throw new FliGenException(ErrorCodes.InvalidPlayerId, $"Invalid playerId - {playerId}");
            }
            if (registrationDate > DateTime.UtcNow)
            {
                throw new FliGenException(ErrorCodes.InvalidDate, $"Invalid registration date - {registrationDate}");
            }

            TourId = tourId;
            PlayerId = playerId;
            RegistrationDate = registrationDate;
        }

        public static TourRegistration Create(int tourId, int playerId, DateTime registrationDate)
        {
            return new TourRegistration(tourId, playerId, registrationDate);
        }
    }
}
