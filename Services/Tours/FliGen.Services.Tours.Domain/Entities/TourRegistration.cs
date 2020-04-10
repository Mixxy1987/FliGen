using System;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;

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
                throw new FliGenException("invalid_tourId", $"Invalid tourId - {tourId}");
            }
            if (playerId <= 0)
            {
                throw new FliGenException("invalid_playerId", $"Invalid playerId - {playerId}");
            }
            if (registrationDate > DateTime.UtcNow)
            {
                throw new FliGenException("invalid_date", $"Invalid registration date - {registrationDate}");
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
