using FliGen.Common.SeedWork;
using FliGen.Common.Types;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class TourRegistration : Entity
    {
        public int TourId { get; set; }
        public int PlayerId { get; set; }

        private TourRegistration()
        { }

        private TourRegistration(int tourId, int playerId)
        {
            if (tourId <= 0)
            {
                throw new FliGenException("invalid_tourId", $"Invalid tourId. - {tourId}");
            }
            if (playerId <= 0)
            {
                throw new FliGenException("invalid_playerId", $"Invalid playerId. - {playerId}");
            }
            TourId = tourId;
            PlayerId = playerId;
        }

        public static TourRegistration Create(int tourId, int playerId)
        {
            return new TourRegistration(tourId, playerId);
        }
    }
}
