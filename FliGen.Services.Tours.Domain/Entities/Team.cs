using FliGen.Common.SeedWork;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public int TeamRoleId
        {
            get => TeamRole.Id;
            set => TeamRole = Enumeration.FromValue<TeamRole>(value);
        }

        public TeamRole TeamRole { get; private set; }

        public int PlayersCount { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}